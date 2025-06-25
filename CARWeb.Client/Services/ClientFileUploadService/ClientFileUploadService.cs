using System.Net.Http.Json;
using CARWeb.Client.Response;
using CARWeb.Client.Utilities;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using static System.Net.WebRequestMethods;

namespace CARWeb.Client.Services.ClientFileUploadService
{
    public class ClientFileUploadService : IClientFileUploadService
    {
        private readonly HttpClient _http;

        public ClientFileUploadService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<string>> UploadFilesViaApi(List<IBrowserFile> files, string subFolder)
        {
            if (files.Count == 0) return new();

            try
            {
                var content = new MultipartFormDataContent();
                var uploadedFileNames = new List<string>();

                foreach (var file in files)
                {
                    var guid = Guid.NewGuid().ToString();
                    var newFileName = $"{Path.GetFileNameWithoutExtension(file.Name)}_{guid}{Path.GetExtension(file.Name)}";

                    var streamContent = new StreamContent(file.OpenReadStream(5 * 1024 * 1024));
                    streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                    content.Add(streamContent, "files", newFileName);

                    uploadedFileNames.Add(newFileName);
                }

                content.Add(new StringContent(subFolder), "subFolder");

                var response = await _http.PostAsync("api/FileUpload/upload", content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<UploadResult>();
                    return result?.Files ?? uploadedFileNames; // fallback in case backend doesn’t echo file names
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UploadFilesViaApi] Exception: {ex}");
            }

            return new();
        }

    }
}
