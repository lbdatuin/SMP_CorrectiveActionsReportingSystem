using CARWeb.Shared.DTOs.UserManagementDTO;
using CARWeb.Shared.Response;

namespace CARWeb.Client.Services.ClientUserManagementService
{
    public interface IClientUserManagementService
    {
        Task<int> CreateUserRole(CreateUserRoleDTO payload);
        Task<int> UpdateUserRole(int Id, CreateUserRoleDTO payload);
        Task<PaginatedTableResponse<GetUserRoleDTO>> GetPaginatedUserRoles(GetPaginatedDTO payload);
    }
}
