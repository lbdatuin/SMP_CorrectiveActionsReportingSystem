using CARWeb.Shared.DTOs.CARLabelDTO;
using CARWeb.Shared.Response;

namespace CARWeb.Services.CARLabelService
{
    public interface ICARLabelService
    {
        Task<int> CreateStandard(CreateStandardDTO request);
        Task<int> UpdateStandard(int Id, CreateStandardDTO request);
        Task<PaginatedTableResponse<GetStandardDTO>> GetPaginatedStandards(GetPaginatedDTO request);

        Task<int> CreateCARType(CreateCARTypeDTO request);
        Task<int> UpdateCARType(int Id, CreateCARTypeDTO request);
        Task<PaginatedTableResponse<GetCARTypeDTO>> GetPaginatedCARTypes(GetPaginatedDTO request);

        Task<int> CreateNonConformity(CreateNonConformityDTO request);
        Task<int> UpdateNonConformity(int Id, CreateNonConformityDTO request);
        Task<PaginatedTableResponse<GetNonConformityDTO>> GetPaginatedNonConformities(GetPaginatedDTO request);
    }
}
