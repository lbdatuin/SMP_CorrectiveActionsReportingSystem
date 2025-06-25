using CARWeb.Response;
using CARWeb.Shared.DTOs.AuthDTO;
using CARWeb.Shared.Models.Auth;

namespace CARWeb.Server.Services.AuthService
{
    public interface IAuthService 
    {
        Task<int> Register(RegisterDTO request);
        Task<LoginResponse> Login(LoginDTO request);
        Task<int> ForgotPass(ForgotPasswordDTO request);
        Task<int> VerifyCode(VerifyCodeDTO request);
        Task<LoginResponse?> ReRefreshToken(string? refToken);
        LoginResponse Logout();
        Task<int> ChangePassword(ChangePassDTO request);
        Task<int> UpdateUser(Guid userId, EditUserDTO request);
        Task<EditProfileDTO?> GetSingleUser();
        Task<string> GetSingleUserAvatar();
    }
}
