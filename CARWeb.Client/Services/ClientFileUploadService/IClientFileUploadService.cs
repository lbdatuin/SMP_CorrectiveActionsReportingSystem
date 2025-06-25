using Microsoft.AspNetCore.Components.Forms;

namespace CARWeb.Client.Services.ClientFileUploadService
{
    public interface IClientFileUploadService
    {
        Task<List<string>> UploadFilesViaApi(List<IBrowserFile> files, string subFolder);
    }
}
