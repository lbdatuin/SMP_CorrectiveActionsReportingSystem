using CARWeb.Services.CARLabelService;
using CARWeb.Shared.DTOs.CARLabelDTO;
using CARWeb.Shared.DTOs.DepartmentSectionDTO;
using CARWeb.Shared.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CARWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CARLabelController : ControllerBase
    {
        private readonly ICARLabelService _cARLabelService;

        public CARLabelController(ICARLabelService CARLabelService)
        {
            _cARLabelService = CARLabelService;
        }

        [HttpPost("add-standard")]
        public async Task<ActionResult<int>> CreateStandard(CreateStandardDTO request)
        {
            int response = await _cARLabelService.CreateStandard(request);
            return response > 0 ? Ok(response) : Unauthorized();
        }

        [HttpPut("update-standard/{Id}")]
        public async Task<ActionResult<int>> UpdateStandard(int Id, CreateStandardDTO request)
        {
            int response = await _cARLabelService.UpdateStandard(Id, request);
            return response > 0 ? Ok(response) : Unauthorized();
        }

        [HttpGet("get-paginated-standard")]
        public async Task<ActionResult<PaginatedTableResponse<GetStandardDTO>>> GetPaginatedStandards([FromQuery] GetPaginatedDTO request)
        {
            PaginatedTableResponse<GetStandardDTO> response = await _cARLabelService.GetPaginatedStandards(request);
            return response.Count > 0 ? Ok(response) : NotFound();
        }


        [HttpPost("add-car-type")]
        public async Task<ActionResult<int>> CreateCARType(CreateCARTypeDTO request)
        {
            int response = await _cARLabelService.CreateCARType(request);
            return response > 0 ? Ok(response) : Unauthorized();
        }
        
        [HttpPut("update-car-type/{Id}")]
        public async Task<ActionResult<int>> UpdateCARType(int Id, CreateCARTypeDTO request)
        {
            int response = await _cARLabelService.UpdateCARType(Id, request);
            return response > 0 ? Ok(response) : Unauthorized();
        }

        [HttpGet("get-paginated-car-type")]
        public async Task<ActionResult<PaginatedTableResponse<GetCARTypeDTO>>> GetPaginatedCARTypes([FromQuery] GetPaginatedDTO request)
        {
            PaginatedTableResponse<GetCARTypeDTO> response = await _cARLabelService.GetPaginatedCARTypes(request);
            return response.Count > 0 ? Ok(response) : NotFound();
        }


        [HttpGet("get-car-type-list")]
        public async Task<ActionResult<List<GetCARTypeDTO>>> GetCarTypeList()
        {
            List<GetCarTypeListDTO> response = await _cARLabelService.GetCarTypeList();
            return Ok(response);
        }

        [HttpPost("add-non-conformity")]
        public async Task<ActionResult<int>> CreateNonConformity(CreateNonConformityDTO request)
        {
            int response = await _cARLabelService.CreateNonConformity(request);
            return response > 0 ? Ok(response) : Unauthorized();
        }

        [HttpPut("update-non-conformity/{Id}")]
        public async Task<ActionResult<int>> UpdateNonConformity(int Id, CreateNonConformityDTO request)
        {
            int response = await _cARLabelService.UpdateNonConformity(Id, request);
            return response > 0 ? Ok(response) : Unauthorized();
        }

        [HttpGet("get-paginated-non-conformity")]
        public async Task<ActionResult<PaginatedTableResponse<GetNonConformityDTO>>> GetPaginatedNonConformities([FromQuery] GetPaginatedDTO request)
        {
            PaginatedTableResponse<GetNonConformityDTO> response = await _cARLabelService.GetPaginatedNonConformities(request);
            return response.Count > 0 ? Ok(response) : NotFound();
        }


    }
}
