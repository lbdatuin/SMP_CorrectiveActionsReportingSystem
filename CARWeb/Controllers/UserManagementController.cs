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

        [HttpGet("get-paginated-users")]
        public async Task<ActionResult<PaginatedTableResponse<GetUsersDTO>>> GetPaginatedUsers([FromQuery] GetPaginatedDTO request)
        {
            PaginatedTableResponse<GetUsersDTO> response = await _userManagementService.GetPaginatedUsers(request);
            return response.Count > 0 ? Ok(response) : NoContent();
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

        [HttpGet("get-role-name-list-by-id")]
        public async Task<ActionResult<List<string>>> GetRoleNameListById([FromQuery] Guid UserId)
        {
            List<string> response = await _userManagementService.GetRoleNameListById(UserId);
            return response.Count > 0 ? Ok(response) : NoContent();
        }

        [HttpGet("get-dept-approver")]
        public async Task<ActionResult<List<string>>> GetDepartmentApprover([FromQuery] int departmentId)
        {
            List<string> response = await _userManagementService.GetDepartmentApprover(departmentId);
            return Ok(response);
        }

        [HttpGet("get-entry-reviewer")]
        public async Task<ActionResult<List<string>>> GetEntryReviewer()
        {
            List<string> response = await _userManagementService.GetEntryReviewer();
            return Ok(response);
        }

        [HttpGet("get-ims-approver")]
        public async Task<ActionResult<List<string>>> GetIMSHeadApprover()
        {
            List<string> response = await _userManagementService.GetIMSHeadApprover();
            return Ok(response);
        }

        [HttpGet("get-reviewer-designation")]
        public async Task<ActionResult<string>> GetReviewerDesignation([FromQuery] string name)
        {
            string response = await _userManagementService.GetReviewerDesignation(name);
            return Ok(response);
        }


    }
}
