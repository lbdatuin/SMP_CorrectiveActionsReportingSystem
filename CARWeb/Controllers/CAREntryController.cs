using CARWeb.Services.CAREntryService;
using CARWeb.Shared.DTOs.CAREntryDTO;
using CARWeb.Shared.DTOs.CARLabelDTO;
using CARWeb.Shared.DTOs.DepartmentSectionDTO;
using CARWeb.Shared.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CARWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CAREntryController : ControllerBase
    {
        private readonly ICAREntryService _cAREntryService;

        public CAREntryController(ICAREntryService CAREntryService)
        {
            _cAREntryService = CAREntryService;
        }

        [HttpPost("add-entry")]
        public async Task<ActionResult<int>> CreateEntry(CreateCARHeaderDTO request)
        {
            int response = await _cAREntryService.CreateEntry(request);
            return response > 0 ? Ok(response) : BadRequest();
        }

        [HttpGet("get-single-entry")]
        public async Task<ActionResult<GetCARHeaderDTO>> GetSingleEntry([FromQuery] int Id)
        {
            var response = await _cAREntryService.GetSingleEntry(Id);
            return response != null ? Ok(response) : NotFound();
        }

        [HttpGet("get-paginated-entry")]
        public async Task<ActionResult<PaginatedTableResponse<GetCARListDTO>>> GetPaginatedEntry([FromQuery] GetPaginatedDTO request)
        {
            PaginatedTableResponse<GetCARListDTO> response = await _cAREntryService.GetPaginatedEntry(request);
            return response.Count > 0 ? Ok(response) : NotFound();
        }

    }
}
