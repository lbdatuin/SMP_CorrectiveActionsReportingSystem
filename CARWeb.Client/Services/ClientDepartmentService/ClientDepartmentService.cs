using CARWeb.Client.Utilities;
using CARWeb.Shared.DTOs.DepartmentSectionDTO;
using CARWeb.Shared.Response;
using MudBlazor;
using System.Net.Http.Json;

namespace CARWeb.Client.Services.ClientDepartmentService
{
    public class ClientDepartmentService : IClientDepartmentService
    {
        private readonly HttpClient _http;
        private readonly ModifiedSnackBar _modifiedSnackBar;
        private readonly SubmitModal _submitModal;

        public ClientDepartmentService(HttpClient http, ModifiedSnackBar modifiedSnackBar, SubmitModal submitModal)
        {
            _http = http;
            _modifiedSnackBar = modifiedSnackBar;
            _submitModal = submitModal;
        }

        public async Task<int> CreateDepartment(CreateDepartmentDTO payload)
        {
            HttpResponseMessage response = await _http.PostAsJsonAsync("api/department/add-department", payload);
            if (response.IsSuccessStatusCode)
            {
                _submitModal.ShowSuccess(null);
                return 1;
            }
            _modifiedSnackBar.ShowMessage("Failed to save Department", Severity.Error);
            return 0;
        }

        public async Task<int> UpdateDepartment(int Id, CreateDepartmentDTO payload)
        {
            HttpResponseMessage response = await _http.PutAsJsonAsync($"api/department/update-department/{Id}", payload);
            if (response.IsSuccessStatusCode)
            {
                _modifiedSnackBar.ShowMessage("Department updated successfully", Severity.Success);
                return 1;
            }
            _modifiedSnackBar.ShowMessage("Failed to update Department", Severity.Error);
            return 0;
        }

        public async Task<PaginatedTableResponse<GetDepartmentDTO>> GetPaginatedDepartments(GetPaginatedDTO payload)
        {
            HttpResponseMessage response = await _http.GetAsync($"api/department/get-paginated-department?Take={payload.Take}&Skip={payload.Skip}&SearchValue={payload.SearchValue}");

            var nullResponse = new PaginatedTableResponse<GetDepartmentDTO>();
            if (response.IsSuccessStatusCode)
            {
                var response_data = await response.Content.ReadFromJsonAsync<PaginatedTableResponse<GetDepartmentDTO>>();
                return response_data ?? nullResponse;
            }
            return nullResponse;
        }
    }
}
