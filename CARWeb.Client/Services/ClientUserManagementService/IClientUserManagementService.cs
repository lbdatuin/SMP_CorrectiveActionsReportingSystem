using CARWeb.Shared.DTOs.UserManagementDTO;
using CARWeb.Shared.Response;

namespace CARWeb.Client.Services.ClientUserManagementService
{
    public interface IClientUserManagementService
    {
        Task<int> CreateUserRole(CreateUserRoleDTO payload);
        Task<int> UpdateUserRole(int Id, CreateUserRoleDTO payload);
        Task<PaginatedTableResponse<GetUserRoleDTO>> GetPaginatedUserRoles(GetPaginatedDTO payload);
        Task<List<GetUserRoleDTO>> GetRoleList();
        Task<List<int>> GetRoleListById(Guid UserId);
        Task<List<string>> GetRoleNameListById(Guid UserId);
    }
}
