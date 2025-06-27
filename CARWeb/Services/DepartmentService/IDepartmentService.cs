using CARWeb.Shared.DTOs.DepartmentSectionDTO;
using CARWeb.Shared.Response;

namespace CARWeb.Services.DepartmentService
{
    public interface IDepartmentService
    {
        Task<int> CreateDepartment(CreateDepartmentDTO request);
        Task<int> UpdateDepartment(int Id, CreateDepartmentDTO request);
        Task<PaginatedTableResponse<GetDepartmentDTO>> GetPaginatedDepartments(GetPaginatedDTO request);
        Task<List<GetDepartmentDTO>> GetDepartmentList();
    }
}
