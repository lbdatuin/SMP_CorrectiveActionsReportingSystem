using CARWeb.Client.Utilities;
using CARWeb.Shared.DTOs.CAREntryDTO;
using CARWeb.Shared.DTOs.DepartmentSectionDTO;
using CARWeb.Shared.Response;
using MudBlazor;
using System.Net.Http.Json;

namespace CARWeb.Client.Services.ClientCAREntryService
{
    public class ClientCAREntryService : IClientCAREntryService
    {
        private readonly HttpClient _http;
        private readonly ModifiedSnackBar _modifiedSnackBar;
        private readonly SubmitModal _submitModal;

        public ClientCAREntryService(HttpClient http, ModifiedSnackBar modifiedSnackBar, SubmitModal submitModal)
        {
            _http = http;
            _modifiedSnackBar = modifiedSnackBar;
            _submitModal = submitModal;
        }

        public async Task<int> CreateEntry(CreateCARHeaderDTO payload)
        {
            HttpResponseMessage response = await _http.PostAsJsonAsync("api/carentry/add-entry", payload);
            if (response.IsSuccessStatusCode)
            {
                _submitModal.ShowSuccess(null);
                return 1;
            }
            _modifiedSnackBar.ShowMessage("Failed to save Entry", Severity.Error);
            return 0;
        }

        public async Task<PaginatedTableResponse<GetCARListDTO>> GetPaginatedEntry(GetPaginatedDTO payload)
        {
            HttpResponseMessage response = await _http.GetAsync($"api/carentry/get-paginated-entry?Take={payload.Take}&Skip={payload.Skip}&SearchValue={payload.SearchValue}");

            var nullResponse = new PaginatedTableResponse<GetCARListDTO>();
            if (response.IsSuccessStatusCode)
            {
                var response_data = await response.Content.ReadFromJsonAsync<PaginatedTableResponse<GetCARListDTO>>();
                return response_data ?? nullResponse;
            }
            return nullResponse;
        }

        public async Task<GetCARHeaderDTO?> GetSingleEntry(int Id)
        {
            HttpResponseMessage response = await _http.GetAsync($"api/carentry/get-single-entry?Id={Id}");

            var nullResponse = new GetCARHeaderDTO();
            if (response.IsSuccessStatusCode)
            {
                var response_data = await response.Content.ReadFromJsonAsync<GetCARHeaderDTO>();
                return response_data ?? nullResponse;
            }
            return nullResponse;
        }
    }
}
