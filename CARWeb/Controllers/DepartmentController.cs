using CARWeb.Services.DepartmentService;
using CARWeb.Shared.DTOs.CARLabelDTO;
using CARWeb.Shared.DTOs.DepartmentSectionDTO;
using CARWeb.Shared.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CARWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService DepartmentService)
        {
            _departmentService = DepartmentService;
        }

        [HttpPost("add-department")]
        public async Task<ActionResult<int>> CreateStandard(CreateDepartmentDTO request)
        {
            int response = await _departmentService.CreateDepartment(request);
            return response > 0 ? Ok(response) : Unauthorized();
        }

        [HttpPut("update-department/{Id}")]
        public async Task<ActionResult<int>> UpdateStandard(int Id, CreateDepartmentDTO request)
        {
            int response = await _departmentService.UpdateDepartment(Id, request);
            return response > 0 ? Ok(response) : Unauthorized();
        }

        [HttpGet("get-paginated-department")]
        public async Task<ActionResult<PaginatedTableResponse<GetDepartmentDTO>>> GetPaginatedDepartments([FromQuery] GetPaginatedDTO request)
        {
            PaginatedTableResponse<GetDepartmentDTO> response = await _departmentService.GetPaginatedDepartments(request);
            return response.Count > 0 ? Ok(response) : NotFound();
        }

    }
}
