using AutoMapper;
using BusinessLayer.Abstract;
using DtoLayer.NewsImageDto;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsImageController : ControllerBase
    {
        private readonly INewsImageService _newsImageService;
        private readonly string _uiRootPath;

        public NewsImageController(INewsImageService newsImageService, IConfiguration configuration)
        {
            _newsImageService = newsImageService;
            _uiRootPath = configuration["UIRootPath"];
        }

        [HttpGet("{newsId}")]
        public IActionResult GetImagesByNewsId(int newsId)
        {
            var images = _newsImageService.TGetListAll()
                .Where(img => img.NewsID == newsId)
                .OrderBy(img => img.Index) // Order images by Index
                .ToList();

            if (images == null || !images.Any())
                return NotFound("Resimler bulunamadı");

            return Ok(images);
        }

        [HttpPost("{newsId}/upload")]
        public async Task<IActionResult> UploadImage(int newsId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Dosya seçilmedi");

            var permittedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                return BadRequest("Yalnızca görüntü dosyaları kabul edilir (jpg, jpeg, png, gif).");

            if (string.IsNullOrEmpty(_uiRootPath))
                return StatusCode(StatusCodes.Status500InternalServerError, "UIRootPath is not configured.");

            var uploadPath = Path.Combine(_uiRootPath, "news-images");

            if (!Directory.Exists(uploadPath))
            {
                try
                {
                    Directory.CreateDirectory(uploadPath);
                }
                catch (Exception ex)
                {
                    // Log error (optional)
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Klasör oluşturulamadı: {ex.Message}");
                }
            }

            var uniqueFileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{Guid.NewGuid()}{extension}";
            var path = Path.Combine(uploadPath, uniqueFileName);

            try
            {
               

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Get the next index
                var maxIndex = _newsImageService.TGetListAll()
                    .Where(img => img.NewsID == newsId)
                    .Select(img => img.Index)
                    .DefaultIfEmpty(0)
                    .Max();

                var newsImage = new NewsImage
                {
                    NewsID = newsId,
                    ImageUrl = uniqueFileName,
                    Index = maxIndex + 1 // Set index to the next available value
                };

                // Add image to database
                _newsImageService.TAdd(newsImage);

                return Ok(new { path });
            }
            catch (Exception ex)
            {
                // Log error (optional)
                return StatusCode(StatusCodes.Status500InternalServerError, $"İşlem başarısız: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateImage(int id, UpdateNewsImageDto updateNewsImageDto)
        {
            var image = _newsImageService.TGetByID(id);
            if (image == null)
                return NotFound("Resim bulunamadı");

            // Update properties
            image.ImageUrl = updateNewsImageDto.ImageUrl ?? image.ImageUrl;
            image.Index = updateNewsImageDto.Index; // Update the index

            _newsImageService.TUpdate(image);

            return Ok("Resim güncellendi");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteImage(int id)
        {
            var image = _newsImageService.TGetByID(id);
            if (image == null)
                return NotFound("Resim bulunamadı");

            _newsImageService.TDelete(image);
            return Ok("Resim silindi");
        }
    }
}
