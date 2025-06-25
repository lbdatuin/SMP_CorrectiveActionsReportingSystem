using CARWeb.Client.Response;
using CARWeb.Client.Utilities;
using CARWeb.Shared.DTOs.AuthDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using MudBlazor;
using System.Net;
using System.Net.Http.Json;

namespace CARWeb.Client.Services.ClientAuthService
{
    public class ClientAuthService : IClientAuthService
    {
        private HttpClient _http;
        private readonly NavigationManager _navigationManager;
        private readonly ModifiedSnackBar _modifiedSnackBar;
        private readonly SubmitModal _submitModal;

        public ClientAuthService(
            HttpClient http,
            NavigationManager navigationManager,
            ModifiedSnackBar modifiedSnackBar,
            SubmitModal submitModal

        )
        {
            _http = http;
            _navigationManager = navigationManager;
            _modifiedSnackBar = modifiedSnackBar;
            _submitModal = submitModal;
        }

        public Token Token { get; set; } = new Token();

        public async Task<string> GetSingleUserAvatar()
        {
            var result = await _http.GetStringAsync("api/auth/single-user-avatar");
            return result;
        }

        public async Task<EditProfileDTO?> GetSingleUser()
        {
            // if provided an Id that does not exist GetAsync returns null
            var result = await _http.GetAsync($"api/auth/get-single-user");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return await result.Content.ReadFromJsonAsync<EditProfileDTO>();
            }
            return null;
        }

        public async Task<int> UpdateUser(Guid userId, EditUserDTO request)
        {
            HttpResponseMessage? response = await _http.PutAsJsonAsync($"api/auth/update-user?userId={userId}", request);

            if (response.IsSuccessStatusCode)
            {
                int response_data = await response.Content.ReadFromJsonAsync<int>();
                if (response_data == 0) return 0;
                _modifiedSnackBar.ShowMessage("User has been updated successfully", Severity.Success);
                return response_data;
            }
            else
            {
                _modifiedSnackBar.ShowMessage("Failed to update user", Severity.Error);
                return 0;
            }
        }

        private async Task<string> SetToken(HttpResponseMessage message)
        {
            if (message.IsSuccessStatusCode)
            {
                Token.value = await message.Content.ReadAsStringAsync();
                return "success";
            }
            else
            {
                return await message.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> LoginAsync(LoginDTO request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/login", request);

            var data = await SetToken(result);

            return data;
        }

        public async Task<string> RefreshToken()
        {
            var result = await _http.PostAsync("api/auth/refresh-token", null);

            string data = await SetToken(result);
            return data;
        }

        public async Task<int> Register(RegisterDTO request)
        {
            HttpResponseMessage? response = await _http.PostAsJsonAsync("api/auth/register", request);

            if (response.IsSuccessStatusCode)
            {
                int response_data = await response.Content.ReadFromJsonAsync<int>();
                if (response_data == 0) return 0;
                _submitModal.ShowSuccess("A new account has been created");
                return response_data;
            }
            else
            {
                return 0;
            }
        }

        public async Task<string> Logout()
        {
            var result = await _http.PostAsync("api/auth/logout", null);

            string data = await SetToken(result);
            return data;
        }

        public async Task<int> ChangePassword(ChangePassDTO payload)
        {
            HttpResponseMessage? response = await _http.PutAsJsonAsync("api/auth/change-pass", payload);

            if (response.IsSuccessStatusCode)
            {
                int response_data = await response.Content.ReadFromJsonAsync<int>();
                if (response_data == 0) return 0;

                _modifiedSnackBar.ShowMessage("Password Updated Successfully", Severity.Success);
                return response_data;
            }
            else
            {
                _modifiedSnackBar.ShowMessage("Failed to Update Password", Severity.Error);
                return 0;
            }
        }

        public async Task<int> ForgotPass(ForgotPasswordDTO payload)
        {
            HttpResponseMessage? response = await _http.PostAsJsonAsync("api/auth/forgot-pass", payload);

            if (response.IsSuccessStatusCode)
            {
                int response_data = await response.Content.ReadFromJsonAsync<int>();
                if (response_data == 0) return 0;
                _modifiedSnackBar.ShowMessage("Password Updated Successfully", Severity.Success);
                _navigationManager.NavigateTo("login");
                return response_data;
            }
            else
            {
                _modifiedSnackBar.ShowMessage("Failed to Update Password", Severity.Error);
                _navigationManager.NavigateTo("login");
                return 0;
            }
        }

        public async Task<int> VerifyCode(VerifyCodeDTO payload)
        {
            HttpResponseMessage? response = await _http.PostAsJsonAsync("api/auth/verify-code", payload);

            if (response.IsSuccessStatusCode)
            {
                int response_data = await response.Content.ReadFromJsonAsync<int>();
                if (response_data == 0) return 0;
                _modifiedSnackBar.ShowMessage("Verification Successful", Severity.Success);
                return response_data;
            }
            else
            {
                _modifiedSnackBar.ShowMessage("Invalid Verification Code", Severity.Error);
                return 0;
            }
        }

    }
}
