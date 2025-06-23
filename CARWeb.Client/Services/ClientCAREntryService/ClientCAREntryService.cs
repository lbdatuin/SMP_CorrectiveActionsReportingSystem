using CARWeb.Client.Utilities;
using CARWeb.Shared.DTOs.CAREntryDTO;
using CARWeb.Shared.DTOs.DepartmentSectionDTO;
using CARWeb.Shared.Response;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace CARWeb.Client.Services.ClientCAREntryService
{
    public class ClientCAREntryService : IClientCAREntryService
    {
        private readonly HttpClient _http;
        private readonly ModifiedSnackBar _modifiedSnackBar;
        private readonly SubmitModal _submitModal;
        private readonly NavigationManager _navigation;

        public ClientCAREntryService(HttpClient http, ModifiedSnackBar modifiedSnackBar, SubmitModal submitModal, NavigationManager navigation)
        {
            _http = http;
            _modifiedSnackBar = modifiedSnackBar;
            _submitModal = submitModal;
            _navigation = navigation;
        }

     

        public async Task<int> CreateEntry(CreateCARHeaderDTO payload)
        {
            HttpResponseMessage response = await _http.PostAsJsonAsync("api/carentry/add-entry", payload);
            if (response.IsSuccessStatusCode)
            {
                _navigation.NavigateTo("/corrective-action");
                await _submitModal.ShowSuccess(null);
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

        public async Task<int> NotifyHead(int Id)
        {
            HttpResponseMessage response = await _http.PutAsync($"api/carentry/notify-head?Id={Id}", null);
            if (response.IsSuccessStatusCode)
            {
                _submitModal.ShowSuccess("Department Head has been notified");
                return 1;
            }
            _modifiedSnackBar.ShowMessage("Failed to notify dept. head", Severity.Error);
            return 0;
        }

        public async Task<int> ReturnEntry(int Id, CreateReturnCommentDTO payload)
        {
            HttpResponseMessage response = await _http.PutAsJsonAsync($"api/carentry/return-entry?Id={Id}", payload);
            if (response.IsSuccessStatusCode)
            {
                _navigation.NavigateTo("/corrective-action");
                await _submitModal.ShowSuccess("Entry has been returned");
                return 1;
            }
            _modifiedSnackBar.ShowMessage("Failed to return entry", Severity.Error);
            return 0;
        }

        public async Task<int> ApproveEntry(int Id)
        {
            HttpResponseMessage response = await _http.PutAsync($"api/carentry/approve-entry?Id={Id}", null);
            if (response.IsSuccessStatusCode)
            {
                _navigation.NavigateTo("/corrective-action");
                await _submitModal.ShowSuccess("Entry has been approved");
                return 1;
            }
            _modifiedSnackBar.ShowMessage("Failed to approve entry", Severity.Error);
            return 0;
        }

        public async Task<int> EditEntry(int Id, CreateCARHeaderDTO payload)
        {
            HttpResponseMessage response = await _http.PutAsJsonAsync($"api/carentry/edit-entry?Id={Id}", payload);
            if (response.IsSuccessStatusCode)
            {
                _navigation.NavigateTo("/corrective-action");
                await _submitModal.ShowSuccess("Entry has been updated");
                return 1;
            }
            _modifiedSnackBar.ShowMessage("Failed to update entry", Severity.Error);
            return 0;
        }

        public async Task<int> ReviewEntry(int Id)
        {
            HttpResponseMessage response = await _http.PutAsync($"api/carentry/review-entry?Id={Id}", null);
            if (response.IsSuccessStatusCode)
            {
                _navigation.NavigateTo("/corrective-action");
                await _submitModal.ShowSuccess("Entry has been reviewed");
                return 1;
            }
            _modifiedSnackBar.ShowMessage("Failed to review entry", Severity.Error);
            return 0;
        }

        public async Task<int> ProceedEntry(int Id, CreateCARHeaderDTO request)
        {
            HttpResponseMessage response = await _http.PutAsJsonAsync($"api/carentry/proceed-entry?Id={Id}", request);
            if (response.IsSuccessStatusCode)
            {
                _navigation.NavigateTo("/corrective-action");
                await _submitModal.ShowSuccess("Entry has been proceed");
                return 1;
            }
            _modifiedSnackBar.ShowMessage("Failed to proceed entry", Severity.Error);
            return 0;
        }


    }
}
