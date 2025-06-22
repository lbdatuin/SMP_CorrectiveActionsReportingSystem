using CARWeb.Services.CAREntryService;
using CARWeb.Shared.DTOs.CAREntryDTO;
using CARWeb.Shared.DTOs.DepartmentSectionDTO;
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

    }
}
