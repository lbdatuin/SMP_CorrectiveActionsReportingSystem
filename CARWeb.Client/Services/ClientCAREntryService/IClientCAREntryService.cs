using CARWeb.Shared.DTOs.CAREntryDTO;

namespace CARWeb.Client.Services.ClientCAREntryService
{
    public interface IClientCAREntryService
    {
        Task<int> CreateEntry(CreateCARHeaderDTO payload);
    }
}
