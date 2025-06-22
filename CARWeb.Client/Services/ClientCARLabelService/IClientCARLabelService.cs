using CARWeb.Shared.DTOs.CARLabelDTO;
using CARWeb.Shared.Response;

namespace CARWeb.Client.Services.ClientCARLabelService
{
    public interface IClientCARLabelService
    {
        Task<int> CreateStandard(CreateStandardDTO payload);
        Task<int> UpdateStandard(int Id, CreateStandardDTO payload);
        Task<PaginatedTableResponse<GetStandardDTO>> GetPaginatedStandards(GetPaginatedDTO payload);
        Task<List<GetStandardListDTO>> GetStandardList();

        Task<int> CreateCARType(CreateCARTypeDTO payload);
        Task<int> UpdateCARType(int Id, CreateCARTypeDTO payload);
        Task<PaginatedTableResponse<GetCARTypeDTO>> GetPaginatedCARTypes(GetPaginatedDTO payload);
        Task<List<GetCarTypeListDTO>> GetCarTypeList();

        Task<int> CreateNonConformity(CreateNonConformityDTO payload);
        Task<int> UpdateNonConformity(int Id, CreateNonConformityDTO payload);
        Task<PaginatedTableResponse<GetNonConformityDTO>> GetPaginatedNonConformities(GetPaginatedDTO payload);
        Task<List<GetNonConformityListDTO>> GetNonConformityList();
    }
}
