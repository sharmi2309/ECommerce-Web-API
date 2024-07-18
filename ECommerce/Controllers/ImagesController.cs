using ECommerce.Data;
using ECommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    [Route("api/Images")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public ImagesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();

                    var image = new Image
                    {
                        FileName = file.FileName,
                        ContentType = GetContentType(Path.GetExtension(file.FileName)?.ToLower()),
                        ImageData = fileBytes
                    };

                    _dbContext.Images.Add(image);
                    await _dbContext.SaveChangesAsync();

                    return Ok($"File uploaded successfully: {file.FileName}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error uploading file: {ex.Message}");
            }
        }

        [HttpGet("Download/{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            try
            {
                var fileEntity = await _dbContext.Images.FirstOrDefaultAsync(x => x.FileName == fileName);
                if (fileEntity == null)
                {
                    return NotFound($"File not found: {fileName}");
                }

                return File(fileEntity.ImageData, fileEntity.ContentType, fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error downloading file: {ex.Message}");
            }
        }

        private string GetContentType(string fileExtension)
        {
            switch (fileExtension)
            {
                case ".jpg":
                    return "image/jpg";
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                default:
                    return null;
            }
        }
    }
}
