using CARWeb.Shared.DTOs.DepartmentSectionDTO;
using CARWeb.Shared.Response;

namespace CARWeb.Client.Services.ClientDepartmentService
{
    public interface IClientDepartmentService
    {
        Task<int> CreateDepartment(CreateDepartmentDTO payload);
        Task<int> UpdateDepartment(int Id, CreateDepartmentDTO payload);
        Task<PaginatedTableResponse<GetDepartmentDTO>> GetPaginatedDepartments(GetPaginatedDTO payload);
        Task<List<GetDepartmentDTO>> GetDepartmentList();
    }
}
