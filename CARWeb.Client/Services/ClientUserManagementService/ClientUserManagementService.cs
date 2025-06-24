using CARWeb.Client.Utilities;
using CARWeb.Shared.DTOs.UserManagementDTO;
using CARWeb.Shared.Response;
using MudBlazor;
using System.Net.Http.Json;

namespace CARWeb.Client.Services.ClientUserManagementService
{
    public class ClientUserManagementService : IClientUserManagementService
    {
        private readonly HttpClient _http;
        private readonly ModifiedSnackBar _modifiedSnackBar;
        private readonly SubmitModal _submitModal;

        public ClientUserManagementService(HttpClient http, ModifiedSnackBar modifiedSnackBar, SubmitModal submitModal)
        {
            _http = http;
            _modifiedSnackBar = modifiedSnackBar;
            _submitModal = submitModal;
        }

        public async Task<int> CreateUserRole(CreateUserRoleDTO payload)
        {
            HttpResponseMessage response = await _http.PostAsJsonAsync("api/usermanagement/add-role", payload);
            if (response.IsSuccessStatusCode)
            {
                _submitModal.ShowSuccess(null);
                return 1;
            }
            _modifiedSnackBar.ShowMessage("Failed to save Role", Severity.Error);
            return 0;
        }

        public async Task<int> UpdateUserRole(int Id, CreateUserRoleDTO payload)
        {
            HttpResponseMessage response = await _http.PutAsJsonAsync($"api/usermanagement/update-role/{Id}", payload);
            if (response.IsSuccessStatusCode)
            {
                _modifiedSnackBar.ShowMessage("Role updated successfully", Severity.Success);
                return 1;
            }
            _modifiedSnackBar.ShowMessage("Failed to update Role", Severity.Error);
            return 0;
        }

        public async Task<PaginatedTableResponse<GetUserRoleDTO>> GetPaginatedUserRoles(GetPaginatedDTO payload)
        {
            HttpResponseMessage response = await _http.GetAsync($"api/usermanagement/get-paginated-role?Take={payload.Take}&Skip={payload.Skip}&SearchValue={payload.SearchValue}");

            var nullResponse = new PaginatedTableResponse<GetUserRoleDTO>();
            if (response.IsSuccessStatusCode)
            {
                var response_data = await response.Content.ReadFromJsonAsync<PaginatedTableResponse<GetUserRoleDTO>>();
                return response_data ?? nullResponse;
            }
            return nullResponse;
        }

        public async Task<PaginatedTableResponse<GetUsersDTO>> GetUsers(GetPaginatedDTO payload)
        {
            HttpResponseMessage response = await _http.GetAsync($"api/usermanagement/get-paginated-user?Take={payload.Take}&Skip={payload.Skip}&SearchValue={payload.SearchValue}");

            var nullResponse = new PaginatedTableResponse<GetUsersDTO>();
            if (response.IsSuccessStatusCode)
            {
                var response_data = await response.Content.ReadFromJsonAsync<PaginatedTableResponse<GetUsersDTO>>();
                return response_data ?? nullResponse;
            }
            return nullResponse;
        }

        public async Task<List<GetUserRoleDTO>> GetRoleList()
        {
            HttpResponseMessage response = await _http.GetAsync($"api/usermanagement/get-role-list");

            var nullResponse = new List<GetUserRoleDTO>();
            if (response.IsSuccessStatusCode)
            {
                var response_data = await response.Content.ReadFromJsonAsync<List<GetUserRoleDTO>>();
                return response_data ?? nullResponse;
            }
            return nullResponse;
        }

        public async Task<List<int>> GetRoleListById(Guid UserId)
        {
            HttpResponseMessage response = await _http.GetAsync($"api/usermanagement/get-role-list-by-id?UserId={UserId}");

            var nullResponse = new List<int>();
            if (response.IsSuccessStatusCode)
            {
                var response_data = await response.Content.ReadFromJsonAsync<List<int>>();
                return response_data ?? nullResponse;
            }
            return nullResponse;
        }

        public async Task<List<string>> GetRoleNameListById(Guid UserId)
        {
            HttpResponseMessage response = await _http.GetAsync($"api/usermanagement/get-role-name-list-by-id?UserId={UserId}");

            var nullResponse = new List<string>();
            if (response.IsSuccessStatusCode)
            {
                var response_data = await response.Content.ReadFromJsonAsync<List<string>>();
                return response_data ?? nullResponse;
            }
            return nullResponse;
        }
    }
}
