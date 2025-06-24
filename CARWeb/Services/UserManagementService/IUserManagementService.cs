using CARWeb.Shared.DTOs.UserManagementDTO;
using CARWeb.Shared.Response;

namespace CARWeb.Services.UserManagementService
{
    public interface IUserManagementService
    {
        Task<int> CreateUserRole(CreateUserRoleDTO request);
        Task<int> UpdateUserRole(int Id, CreateUserRoleDTO request);
        Task<PaginatedTableResponse<GetUserRoleDTO>> GetPaginatedUserRoles(GetPaginatedDTO request);
        Task<PaginatedTableResponse<GetUsersDTO>> GetUsers(GetPaginatedDTO request);
        Task<List<GetUserRoleDTO>> GetRoleList();
        Task<List<int>> GetRoleListById(Guid UserId);
        Task<List<string>> GetRoleNameListById(Guid UserId);
    }
}
