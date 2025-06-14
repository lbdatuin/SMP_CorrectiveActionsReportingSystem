using CARWeb.Shared.DTOs.CARLabelDTO;
using CARWeb.Shared.Response;

namespace CARWeb.Client.Services.ClientCARLabelService
{
    public interface IClientCARLabelService
    {
        Task<int> CreateStandard(CreateStandardDTO payload);
        Task<int> UpdateStandard(int Id, CreateStandardDTO payload);
        Task<PaginatedTableResponse<GetStandardDTO>> GetPaginatedStandards(GetPaginatedDTO payload);

        Task<int> CreateCARType(CreateCARTypeDTO payload);
        Task<int> UpdateCARType(int Id, CreateCARTypeDTO payload);
        Task<PaginatedTableResponse<GetCARTypeDTO>> GetPaginatedCARTypes(GetPaginatedDTO payload);

        Task<int> CreateNonConformity(CreateNonConformityDTO payload);
        Task<int> UpdateNonConformity(int Id, CreateNonConformityDTO payload);
        Task<PaginatedTableResponse<GetNonConformityDTO>> GetPaginatedNonConformities(GetPaginatedDTO payload);
    }
}
