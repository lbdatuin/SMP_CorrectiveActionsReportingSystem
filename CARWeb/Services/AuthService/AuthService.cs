using CARWeb.Data;
using CARWeb.Response;
using CARWeb.Shared.DTOs.AuthDTO;
using CARWeb.Shared.Enums;
using CARWeb.Shared.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CARWeb.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(DataContext dataContext, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = dataContext;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        private string? GetUserId() => _httpContextAccessor.HttpContext?.User
            .FindFirstValue(ClaimTypes.NameIdentifier);

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private EditProfileDTO ConvertProfile(UserDetails request)
        {
            return new EditProfileDTO
            {
                Username = request.User.Username,
                UserFirstName = request.FirstName,
                UserLastName = request.LastName,
                UserPhone = request.Phone,
                UserAddress = request.Address,
                Avatar = request.Avatar
            };
        }

        public async Task<string> GetSingleUserAvatar()
        {
            var users = await _context.UserDetails
                     .Where(p => p.UserId.ToString() == GetUserId())
                      .Select(p => p.Avatar)
                     .FirstOrDefaultAsync();

            return users;
        }
        public async Task<int> UpdateUser(EditProfileDTO request)
        {
            var user = await _context.UserDetails
                .Include(u => u.User)
                .Where(u => u.UserId.ToString() == GetUserId())
                .FirstOrDefaultAsync();

            if (user == null)
                return -1;

            user.User.Username = request.Username;
            user.FirstName = request.UserFirstName;
            user.LastName = request.UserLastName;
            user.Phone = request.UserPhone;
            user.Address = request.UserAddress;
            user.Avatar = request.Avatar;

            int status = await _context.SaveChangesAsync();
            if (status == 0) return 0;

            return 1;
        }

        public async Task<EditProfileDTO?> GetSingleUser()
        {
            var user = await _context.UserDetails
               .Include(u => u.User)   
               .Where(u => u.UserId.ToString() == GetUserId())
               .FirstOrDefaultAsync();
            if (user == null) { return null; }
            return ConvertProfile(user);
        }

        private async Task<string> CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.UserData, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AccessToken").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private ClaimsPrincipal? ValidateRefreshToken(string refreshToken)
        {
            try
            {
                var validationParams = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetValue<string>("RefreshToken")!)),
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
                return new JwtSecurityTokenHandler().ValidateToken
                (
                    refreshToken,
                    validationParams,
                    out SecurityToken token
                );
            }
            catch (Exception e)
            {
                return null;
            }
        }
        private async Task<RefreshToken> GenerateRefreshToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.UserData, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetValue<string>("RefreshToken")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return new RefreshToken
            {
                Token = jwt,
                ExpiresAt = DateTime.Now.AddDays(7)
            };
        }

        private LoginResponse DeleteRefreshToken()
        {
            LoginResponse response = new LoginResponse();

            CookieOptions cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now
            };

            response.CookieOptions = cookieOptions;
            response.AccessToken = string.Empty;
            response.RefreshToken = string.Empty;

            return response;
        }

        public LoginResponse Logout()
        {
            return DeleteRefreshToken();
        }

        public async Task<LoginResponse> Login(LoginDTO request)
        {
            User? user = await _context.Users
                 .FirstOrDefaultAsync(user => user.Username == request.Username && user.IsActive == true);

            LoginResponse response = new LoginResponse();

            if (
                user != null
                && VerifyPasswordHash(request.Password, user.Password, user.PasswordSalt)
            )
            {
                string token = await CreateToken(user);
                RefreshToken refreshToken = await GenerateRefreshToken(user);

                CookieOptions cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = refreshToken.ExpiresAt
                };

                user.RefreshToken = refreshToken.Token;
                user.RefreshTokenCreatedAt = refreshToken.CreatedAt;
                user.RefreshTokenExpiresAt = refreshToken.ExpiresAt;

                await _context.SaveChangesAsync();

                response.CookieOptions = cookieOptions;
                response.AccessToken = token;
                response.RefreshToken = refreshToken.Token;

                return response;
            }
            return response;
        }

        public async Task<int> Register(RegisterDTO request)
        {
            if (await _context.Users.AnyAsync(user => user.Username == request.Username)) return 0;

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User new_user = new User
            {
                Email = request.Email,
                IsActive = request.IsActive,
                Username = request.Username,
                Password = passwordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = new Random().Next(100000, 1000000).ToString(),
                Role = request.Role,
                AccessRoles = request.AccessRoles != null ? request.AccessRoles.Select(role => new AccessRole
                {
                    UserRoleId = role.UserRoleId
                }).ToList() : new List<AccessRole>()
            };
            _context.Users.Add(new_user);

            var userDetails = new UserDetails
            {
                User = new_user,
                UserId = new_user.Id,
                FirstName = request.UserFirstName,
                LastName = request.UserLastName,
            };

            _context.UserDetails.Add(userDetails);

            int status = await _context.SaveChangesAsync();

            return status;
        }

        public async Task<LoginResponse?> ReRefreshToken(string? refToken)
        {
            if (refToken == null) return null;

            var claims = ValidateRefreshToken(refToken);
            if (claims == null) return null;

            string? userIdString = claims.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))?.Value;

            User? db_user = await _context.Users.FirstOrDefaultAsync(user => user.Id.ToString() == userIdString);
            if (db_user == null) return null;

            if (!db_user.RefreshToken.Equals(refToken))
            {
                return null;
            }
            else if (db_user.RefreshTokenExpiresAt < DateTime.Now)
            {
                var res = DeleteRefreshToken();

                return res;
            }

            LoginResponse response = new LoginResponse();

            string token = await CreateToken(db_user);
            RefreshToken refreshToken = await GenerateRefreshToken(db_user);

            CookieOptions cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.ExpiresAt
            };

            db_user.RefreshToken = refreshToken.Token;
            db_user.RefreshTokenCreatedAt = refreshToken.CreatedAt;
            db_user.RefreshTokenExpiresAt = refreshToken.ExpiresAt;

            await _context.SaveChangesAsync();

            response.CookieOptions = cookieOptions;
            response.AccessToken = token;
            response.RefreshToken = refreshToken.Token;

            return response;
        }

        public async Task<int> ChangePassword(ChangePassDTO request)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userIdGuid = Guid.TryParse(userId, out Guid parsedGuid) ? parsedGuid : Guid.Empty;

            User? db_user = await _context.Users
                .FirstOrDefaultAsync(user => user.Id == userIdGuid);

            if (db_user == null) return 0;

            if (
                db_user != null
                && VerifyPasswordHash(request.CurrPassword, db_user.Password, db_user.PasswordSalt)
            )
            {
                CreatePasswordHash(request.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);

                db_user.Password = passwordHash;
                db_user.PasswordSalt = passwordSalt;
                await _context.SaveChangesAsync();
                return 1;

            }
            return 0;
        }

        public async Task<int> ForgotPass(ForgotPasswordDTO request)
        {
            User? dbUser = await _context.Users.
               FirstOrDefaultAsync(user => user.Email == request.Email && user.VerificationToken == request.Code);

            if (dbUser == null) return 0;

            CreatePasswordHash(request.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);

            dbUser.PasswordSalt = passwordSalt;
            dbUser.Password = passwordHash;
            dbUser.VerificationToken = new Random().Next(100000, 1000000).ToString();

            int status = await _context.SaveChangesAsync();
            return status;
        }

        public async Task<int> VerifyCode(VerifyCodeDTO request)
        {
            var verificationToken = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email && u.VerificationToken == request.Code);
            if (verificationToken == null) return 0;
            return 1;
        }


    }
}
