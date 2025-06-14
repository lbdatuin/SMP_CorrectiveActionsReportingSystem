using CARWeb.Client.Response;
using CARWeb.Shared.DTOs.AuthDTO;

namespace CARWeb.Client.Services.ClientAuthService
{
    public interface IClientAuthService
    {
        Token Token { get; set; }
        Task<string> LoginAsync(LoginDTO request);
        Task<string> RefreshToken();
        Task<int> Register(RegisterDTO request);
        Task<string> Logout();
        Task<int> ChangePassword(ChangePassDTO payload);
        Task<int> ForgotPass(ForgotPasswordDTO payload);
        Task<int> VerifyCode(VerifyCodeDTO payload);
        Task<EditProfileDTO?> GetSingleUser();
        Task<int> UpdateUser(EditProfileDTO request);
        Task<string> GetSingleUserAvatar();
    }
}
