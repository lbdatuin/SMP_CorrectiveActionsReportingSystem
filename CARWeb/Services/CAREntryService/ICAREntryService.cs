using CARWeb.Shared.DTOs.CAREntryDTO;
using CARWeb.Shared.DTOs.CARLabelDTO;

namespace CARWeb.Services.CAREntryService
{
    public interface ICAREntryService
    {
        Task<int> CreateEntry(CreateCARHeaderDTO request);
    }
}
