using CARWeb.Data;
using CARWeb.Shared.DTOs.UserManagementDTO;
using CARWeb.Shared.Models.Auth;
using CARWeb.Shared.Models.DepartmentSection;
using CARWeb.Shared.Response;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CARWeb.Services.UserManagementService
{
    public class UserManagementService : IUserManagementService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserManagementService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        private string? GetUserId() => _httpContextAccessor.HttpContext?.User
          .FindFirstValue(ClaimTypes.NameIdentifier);

        public async Task<int> CreateUserRole(CreateUserRoleDTO request)
        {
            try
            {
                string userId = GetUserId();
                if (string.IsNullOrEmpty(userId)) return -1;

                UserRole payload = new UserRole
                {
                    Role = request.Role,
                    Desciption = request.Desciption,
                    CreatedBy = userId,
                    DateCreated = DateTime.Now,
                };
                _context.UserRoles.Add(payload);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> UpdateUserRole(int Id, CreateUserRoleDTO request)
        {
            try
            {
                string userId = GetUserId();
                if (string.IsNullOrEmpty(userId)) return -1;

                UserRole? query = await _context.UserRoles
                    .FirstOrDefaultAsync(q => q.Id == Id);

                if (query == null) return 0;

                query.Role = request.Role;
                query.Desciption = request.Desciption;
                query.ModifiedBy = userId;
                query.DateModified = DateTime.Now;

                return await _context.SaveChangesAsync() == 0 ? 0 : 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<PaginatedTableResponse<GetUserRoleDTO>> GetPaginatedUserRoles(GetPaginatedDTO request)
        {
            PaginatedTableResponse<GetUserRoleDTO> response = new PaginatedTableResponse<GetUserRoleDTO>();

            try
            {
                IQueryable<UserRole> query = _context.UserRoles
                    .OrderByDescending(q => q.Id);

                if (!string.IsNullOrEmpty(request.SearchValue))
                {
                    query = query.Where(q => q.Id.ToString() == request.SearchValue
                    || q.Role.Contains(request.SearchValue));
                }

                List<UserRole> db_result = await query
                    .Skip(request.Skip)
                    .Take(request.Take)
                    .ToListAsync();

                response.Count = await query.CountAsync();
                response.ResponseData = db_result.Select(q => new GetUserRoleDTO
                {
                    Id = q.Id,
                    Role = q.Role,
                    Desciption = q.Desciption
                }).ToList();
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }
        private GetUsersDTO ConvertUserDTO(UserDetails request)
        {
            return new GetUsersDTO
            {
                Id = request.Id,
                Email = request.User.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.User.Username == string.Empty ? "-" : request.User.Username,
                IsActive = request.User.IsActive,
                Role = request.User.Role,
            };
        }

        public async Task<PaginatedTableResponse<GetUsersDTO>> GetUsers(GetPaginatedDTO request)
        {
            IQueryable<UserDetails> query = _context.UserDetails
                .Include(q => q.User)
                .OrderByDescending(q => q.Id);

            if (!string.IsNullOrEmpty(request.SearchValue))
            {
                query = query.Where(q => q.FirstName.Contains(request.SearchValue) || q.LastName.Contains(request.SearchValue));
            }

            int totalCount = await query.CountAsync();

            var paginatedResult = await query
                .Skip(request.Skip)
                .Take(request.Take)
                .ToListAsync();

            var result = paginatedResult.Select(res => ConvertUserDTO(res)).ToList();

            return new PaginatedTableResponse<GetUsersDTO>
            {
                ResponseData = result,
                Count = totalCount
            };
        }

        public async Task<List<GetUserRoleDTO>> GetRoleList()
        {
            List<GetUserRoleDTO> response = new List<GetUserRoleDTO>();

            try
            {
                var query = await _context.UserRoles
                    .ToListAsync();

                response = query.Select(q => new GetUserRoleDTO
                {
                    Id = q.Id,
                    Role = q.Role
                }).ToList();    

                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }

        public async Task<List<int>> GetRoleListById(Guid UserId)
        {
            try
            {
                var query = await _context.AccessRoles
                    .Include(q => q.User)
                    .Include(q => q.UserRole)
                    .Where(q => q.UserId == UserId)
                    .Select(q => q.UserRole.Id)
                    .ToListAsync();

                return query;
            }
            catch (Exception ex)
            {
                return new List<int>();
            }
        }

        public async Task<List<string>> GetRoleNameListById(Guid UserId)
        {
            try
            {
                var query = await _context.AccessRoles
                    .Include(q => q.User)
                    .Include(q => q.UserRole)
                    .Where(q => q.UserId == UserId)
                    .Select(q => q.UserRole.Role)
                    .ToListAsync();

                return query;
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }
    }
}
