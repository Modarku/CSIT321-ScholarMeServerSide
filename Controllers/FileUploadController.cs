using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ScholarMeServer.Controllers
{
    // https://elguerre.com/2024/09/04/proper-handling-of-file-uploads-with-asp-net-core-and-swagger/
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController : ControllerBase
    {
        private readonly ILogger<FileUploadController> _logger;
        private readonly string _fileUploadPath;

        public FileUploadController(ILogger<FileUploadController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _fileUploadPath = configuration.GetValue<string>("FileUploadPath");
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty");

            // Ensure the upload directory exists
            if (!Directory.Exists(_fileUploadPath))
            {
                Directory.CreateDirectory(_fileUploadPath);
            }

            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(_fileUploadPath, uniqueFileName);
            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }

            _logger.LogInformation($"Received file {file.FileName} - size: {file.Length} bytes");

            var serveFileUrl = Url.Action("ServeFile", new { fileName = uniqueFileName });
            return Ok(new { fileName = uniqueFileName, size = file.Length, path = serveFileUrl });
        }

        [HttpGet("serve/{fileName}")]
        public IActionResult ServeFile(string fileName)
        {
            var filePath = Path.Combine(_fileUploadPath, fileName);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileExtension = Path.GetExtension(fileName).ToLowerInvariant();
            var contentType = fileExtension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream"
            };

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, contentType);
        }
    }
}