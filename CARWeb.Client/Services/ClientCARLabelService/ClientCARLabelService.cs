using CARWeb.Client.Utilities;
using CARWeb.Shared.DTOs.CARLabelDTO;
using CARWeb.Shared.DTOs.DepartmentSectionDTO;
using CARWeb.Shared.Response;
using MudBlazor;
using System.Net.Http.Json;

namespace CARWeb.Client.Services.ClientCARLabelService
{
    public class ClientCARLabelService : IClientCARLabelService
    {
        private readonly HttpClient _http;
        private readonly ModifiedSnackBar _modifiedSnackBar;
        private readonly SubmitModal _submitModal;

        public ClientCARLabelService(HttpClient http, ModifiedSnackBar modifiedSnackBar, SubmitModal submitModal)
        {
            _http = http;
            _modifiedSnackBar = modifiedSnackBar;
            _submitModal = submitModal;
        }

        public async Task<int> CreateStandard(CreateStandardDTO payload)
        {
            HttpResponseMessage response = await _http.PostAsJsonAsync("api/carlabel/add-standard", payload);
            if (response.IsSuccessStatusCode)
            {
                _submitModal.ShowSuccess(null);
                return 1;
            }
            _modifiedSnackBar.ShowMessage("Failed to save Standard", Severity.Error);
            return 0;
        }

        public async Task<int> UpdateStandard(int Id, CreateStandardDTO payload)
        {
            HttpResponseMessage response = await _http.PutAsJsonAsync($"api/carlabel/update-standard/{Id}", payload);
            if (response.IsSuccessStatusCode)
            {
                _modifiedSnackBar.ShowMessage("Standard updated successfully", Severity.Success);
                return 1;
            }
            _modifiedSnackBar.ShowMessage("Failed to update Standard", Severity.Error);
            return 0;
        }

        public async Task<PaginatedTableResponse<GetStandardDTO>> GetPaginatedStandards(GetPaginatedDTO payload)
        {
            HttpResponseMessage response = await _http.GetAsync($"api/carlabel/get-paginated-standard?Take={payload.Take}&Skip={payload.Skip}&SearchValue={payload.SearchValue}");

            var nullResponse = new PaginatedTableResponse<GetStandardDTO>();
            if (response.IsSuccessStatusCode)
            {
                var response_data = await response.Content.ReadFromJsonAsync<PaginatedTableResponse<GetStandardDTO>>();
                return response_data ?? nullResponse;
            }
            return nullResponse;
        }


        public async Task<int> CreateCARType(CreateCARTypeDTO payload)
        {
            HttpResponseMessage response = await _http.PostAsJsonAsync("api/carlabel/add-car-type", payload);
            if (response.IsSuccessStatusCode)
            {
                _submitModal.ShowSuccess(null);
                return 1;
            }
            _modifiedSnackBar.ShowMessage("Failed to save car type", Severity.Error);
            return 0;
        }

        public async Task<int> UpdateCARType(int Id, CreateCARTypeDTO payload)
        {
            HttpResponseMessage response = await _http.PutAsJsonAsync($"api/carlabel/update-car-type/{Id}", payload);
            if (response.IsSuccessStatusCode)
            {
                _modifiedSnackBar.ShowMessage("Entry has been updated successfully", Severity.Success);
                return 1;
            }
            _modifiedSnackBar.ShowMessage("Failed to update car type", Severity.Error);
            return 0;
        }

        public async Task<PaginatedTableResponse<GetCARTypeDTO>> GetPaginatedCARTypes(GetPaginatedDTO payload)
        {
            HttpResponseMessage response = await _http.GetAsync($"api/carlabel/get-paginated-car-type?Take={payload.Take}&Skip={payload.Skip}&SearchValue={payload.SearchValue}");

            var nullResponse = new PaginatedTableResponse<GetCARTypeDTO>();
            if (response.IsSuccessStatusCode)
            {
                var response_data = await response.Content.ReadFromJsonAsync<PaginatedTableResponse<GetCARTypeDTO>>();
                return response_data ?? nullResponse;
            }
            return nullResponse;
        }


        public async Task<int> CreateNonConformity(CreateNonConformityDTO payload)
        {
            HttpResponseMessage response = await _http.PostAsJsonAsync("api/carlabel/add-non-conformity", payload);
            if (response.IsSuccessStatusCode)
            {
                _submitModal.ShowSuccess(null);
                return 1;
            }
            _modifiedSnackBar.ShowMessage("Failed to save non conformity", Severity.Error);
            return 0;
        }

        public async Task<int> UpdateNonConformity(int Id, CreateNonConformityDTO payload)
        {
            HttpResponseMessage response = await _http.PutAsJsonAsync($"api/carlabel/update-non-conformity/{Id}", payload);
            if (response.IsSuccessStatusCode)
            {
                _modifiedSnackBar.ShowMessage("Entry has been updated successfully", Severity.Success);
                return 1;
            }
            _modifiedSnackBar.ShowMessage("Failed to update non conformity", Severity.Error);
            return 0;
        }

        public async Task<PaginatedTableResponse<GetNonConformityDTO>> GetPaginatedNonConformities(GetPaginatedDTO payload)
        {
            HttpResponseMessage response = await _http.GetAsync($"api/carlabel/get-paginated-non-conformity?Take={payload.Take}&Skip={payload.Skip}&SearchValue={payload.SearchValue}");

            var nullResponse = new PaginatedTableResponse<GetNonConformityDTO>();
            if (response.IsSuccessStatusCode)
            {
                var response_data = await response.Content.ReadFromJsonAsync<PaginatedTableResponse<GetNonConformityDTO>>();
                return response_data ?? nullResponse;
            }
            return nullResponse;
        }

    }
}
