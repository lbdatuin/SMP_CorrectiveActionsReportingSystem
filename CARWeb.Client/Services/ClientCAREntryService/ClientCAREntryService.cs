using CARWeb.Client.Utilities;
using CARWeb.Shared.DTOs.CAREntryDTO;
using CARWeb.Shared.DTOs.DepartmentSectionDTO;
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
    }
}
