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

        //[HttpGet("image-proxy")]
        //public async Task<IActionResult> GetImage(string path)
        //{
        //    var filePath = Path.Combine(@"\\192.168.0.199\WeighHandshake\car_file_upload", path);
        //    if (!System.IO.File.Exists(filePath))
        //        return NotFound();

        //    var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
        //    return File(bytes, "image/png"); // Or infer MIME type dynamically
        //}


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

        [HttpPut("notify-head")]
        public async Task<ActionResult<int>> NotifyHead([FromQuery] int Id)
        {
            int response = await _cAREntryService.NotifyHead(Id);
            return response > 0 ? Ok(response) : BadRequest();
        }

        [HttpPut("return-entry")]
        public async Task<ActionResult<int>> ReturnEntry([FromQuery] int Id, [FromBody] CreateReturnCommentDTO request)
        {
            int response = await _cAREntryService.ReturnEntry(Id, request);
            return response > 0 ? Ok(response) : BadRequest();
        }

        [HttpPut("approve-entry")]
        public async Task<ActionResult<int>> ApproveEntry([FromQuery] int Id)
        {
            int response = await _cAREntryService.ApproveEntry(Id);
            return response > 0 ? Ok(response) : BadRequest();
        }

        [HttpPut("edit-entry")]
        public async Task<ActionResult<int>> EditEntry([FromQuery] int Id, [FromBody] CreateCARHeaderDTO request)
        {
            int response = await _cAREntryService.EditEntry(Id, request);
            return response > 0 ? Ok(response) : BadRequest();
        }

        [HttpPut("review-entry")]
        public async Task<ActionResult<int>> ReviewEntry([FromQuery] int Id)
        {
            int response = await _cAREntryService.ReviewEntry(Id);
            return response > 0 ? Ok(response) : BadRequest();
        }

        [HttpPut("proceed-entry")]
        public async Task<ActionResult<int>> ProceedEntry([FromQuery] int Id, [FromBody] CreateCARHeaderDTO request)
        {
            int response = await _cAREntryService.ProceedEntry(Id, request);
            return response > 0 ? Ok(response) : BadRequest();
        }

    }
}
