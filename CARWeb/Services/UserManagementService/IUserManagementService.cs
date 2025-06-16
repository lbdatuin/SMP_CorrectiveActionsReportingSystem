using CARWeb.Shared.DTOs.UserManagementDTO;
using CARWeb.Shared.Response;

namespace CARWeb.Services.UserManagementService
{
    public interface IUserManagementService
    {
        Task<int> CreateUserRole(CreateUserRoleDTO request);
        Task<int> UpdateUserRole(int Id, CreateUserRoleDTO request);
        Task<PaginatedTableResponse<GetUserRoleDTO>> GetPaginatedUserRoles(GetPaginatedDTO request);
    }
}
