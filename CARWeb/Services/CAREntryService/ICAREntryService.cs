using CARWeb.Shared.DTOs.CAREntryDTO;
using CARWeb.Shared.DTOs.CARLabelDTO;
using CARWeb.Shared.DTOs.DepartmentSectionDTO;
using CARWeb.Shared.Response;

namespace CARWeb.Services.CAREntryService
{
    public interface ICAREntryService
    {
        Task<int> CreateEntry(CreateCARHeaderDTO request);
        Task<GetCARHeaderDTO?> GetSingleEntry(int Id);
        Task<PaginatedTableResponse<GetCARListDTO>> GetPaginatedEntry(GetPaginatedDTO request);
    }
}
