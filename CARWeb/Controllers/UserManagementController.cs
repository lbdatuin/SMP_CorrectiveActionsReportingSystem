using CARWeb.Services.UserManagementService;
using CARWeb.Shared.DTOs.DepartmentSectionDTO;
using CARWeb.Shared.DTOs.UserManagementDTO;
using CARWeb.Shared.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CARWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;

        public UserManagementController(IUserManagementService UserManagementService)
        {
            _userManagementService = UserManagementService;
        }

        [HttpPost("add-role")]
        public async Task<ActionResult<int>> CreateUserRole(CreateUserRoleDTO request)
        {
            int response = await _userManagementService.CreateUserRole(request);
            return response > 0 ? Ok(response) : Unauthorized();
        }

        [HttpPut("update-role/{Id}")]
        public async Task<ActionResult<int>> UpdateUserRole(int Id, CreateUserRoleDTO request)
        {
            int response = await _userManagementService.UpdateUserRole(Id, request);
            return response > 0 ? Ok(response) : Unauthorized();
        }

        [HttpGet("get-paginated-role")]
        public async Task<ActionResult<PaginatedTableResponse<GetUserRoleDTO>>> GetPaginatedUserRoles([FromQuery] GetPaginatedDTO request)
        {
            PaginatedTableResponse<GetUserRoleDTO> response = await _userManagementService.GetPaginatedUserRoles(request);
            return response.Count > 0 ? Ok(response) : NotFound();
        }

        [HttpGet("get-role-list")]
        public async Task<ActionResult<List<GetUserRoleDTO>>> GetRoleList()
        {
            List<GetUserRoleDTO> response = await _userManagementService.GetRoleList();
            return response.Count > 0 ? Ok(response) : NoContent();
        }

        [HttpGet("get-role-list-by-id")]
        public async Task<ActionResult<List<int>>> GetRoleListById([FromQuery] Guid UserId)
        {
            List<int> response = await _userManagementService.GetRoleListById(UserId);
            return response.Count > 0 ? Ok(response) : NoContent();
        }
    }
}
