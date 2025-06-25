using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CARWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController : ControllerBase
    {
        private readonly long _maxFileSize = 5 * 1024 * 1024; // 5MB
        private readonly string _baseUploadPath = @"\\192.168.0.199\WeighHandshake\car_file_upload";

        [HttpPost("upload")]
        public async Task<IActionResult> UploadDetailsOfIssue([FromForm] List<IFormFile> files, [FromForm] string subFolder)
        {
            if (files == null || files.Count == 0)
                return BadRequest("No files provided.");

            var savedFilePaths = new List<string>();
            var folderPath = Path.Combine(_baseUploadPath, subFolder.Replace(" ", ""));

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            foreach (var file in files)
            {
                if (file.Length > _maxFileSize)
                {
                    return BadRequest($"{file.FileName} exceeds the 5MB size limit.");
                }

                var filePath = Path.Combine(folderPath, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                savedFilePaths.Add(file.FileName);
            }

            return Ok(new { message = "Upload successful", files = savedFilePaths });
        }


    }

}
