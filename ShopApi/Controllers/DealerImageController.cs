using BusinessLayer.Abstract;
using DtoLayer.DealerImageDto;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealerImageController : ControllerBase
    {
        private readonly IDealerImageService _dealerImageService;
        private readonly string _uiRootPath;

        public DealerImageController(IDealerImageService dealerImageService, IConfiguration configuration)
        {
            _dealerImageService = dealerImageService;
            _uiRootPath = configuration["UIRootPath"];
        }

        [HttpGet("{dealerId}")]
        public IActionResult GetImagesByDealerId(int dealerId)
        {
            var images = _dealerImageService.TGetListAll()
                .Where(img => img.DealerID == dealerId)
                .OrderBy(img => img.Index) // Resimleri index'e göre sırala
                .ToList();

            if (images == null || !images.Any())
                return NotFound("Resimler bulunamadı");

            return Ok(images);
        }

        [HttpPost("{dealerId}/upload")]
        public async Task<IActionResult> UploadImage(int dealerId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Dosya seçilmedi");

            var permittedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                return BadRequest("Yalnızca görüntü dosyaları kabul edilir (jpg, jpeg, png, gif).");

            if (string.IsNullOrEmpty(_uiRootPath))
                return StatusCode(StatusCodes.Status500InternalServerError, "UIRootPath is not configured.");

            var uploadPath = Path.Combine(_uiRootPath, "dealer-images");

            if (!Directory.Exists(uploadPath))
            {
                try
                {
                    Directory.CreateDirectory(uploadPath);
                }
                catch (Exception ex)
                {
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

                // Sonraki index'i al
                var maxIndex = _dealerImageService.TGetListAll()
                    .Where(img => img.DealerID == dealerId)
                    .Select(img => img.Index)
                    .DefaultIfEmpty(0)
                    .Max();

                var dealerImage = new DealerImage
                {
                    DealerID = dealerId,
                    ImageUrl = uniqueFileName,
                    Index = maxIndex + 1 // Bir sonraki boş index'i ayarla
                };

                _dealerImageService.TAdd(dealerImage);

                return Ok(new { path });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"İşlem başarısız: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateImage(int id, UpdateDealerImageDto updateDealerImageDto)
        {
            var image = _dealerImageService.TGetByID(id);
            if (image == null)
                return NotFound("Resim bulunamadı");

            image.ImageUrl = updateDealerImageDto.ImageUrl ?? image.ImageUrl;
            image.Index = updateDealerImageDto.Index;

            _dealerImageService.TUpdate(image);

            return Ok("Resim güncellendi");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteImage(int id)
        {
            var image = _dealerImageService.TGetByID(id);
            if (image == null)
                return NotFound("Resim bulunamadı");

            _dealerImageService.TDelete(image);
            return Ok("Resim silindi");
        }
    }
}
