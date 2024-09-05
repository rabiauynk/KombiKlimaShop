using AutoMapper;
using BusinessLayer.Abstract;
using DtoLayer.NewsDto;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace ShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly IMapper _mapper;
        private readonly string _uiRootPath;

        public NewsController(INewsService newsService, IMapper mapper, IConfiguration configuration)
        {
            _newsService = newsService;
            _mapper = mapper;
            _uiRootPath = configuration["UIRootPath"];
        
    }

        [HttpGet]
        public IActionResult NewsList()
        {
            var value = _mapper.Map<List<ResultNewsDto>>(_newsService.TGetListAll());
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateNews(CreateNewsDto createNewsDto)
        {
            var news = new News
            {
                NewTitle = createNewsDto.NewTitle,
                Description = createNewsDto.Description,
                ImageUrls = createNewsDto.ImageUrls,
                NewDate = createNewsDto.NewDate,
                NewDetail = createNewsDto.NewDetail
            };

            _newsService.TAdd(news);
            return Ok("Haberler Eklendi");
        }

        [HttpPost("{id}/upload")]
        public async Task<IActionResult> UploadNewsImage(int id, IFormFile file)
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

                var news = _newsService.TGetByID(id);
                if (news == null)
                    return NotFound("Haber bulunamadı");

                if (news.ImageUrls == null)
                {
                    news.ImageUrls = new List<string>();
                }

                // Append new image URL to existing list
                news.ImageUrls.Add(uniqueFileName);
                _newsService.TUpdate(news);

                return Ok(new { path });
            }
            catch (Exception ex)
            {
                // Log error (optional)
                return StatusCode(StatusCodes.Status500InternalServerError, $"İşlem başarısız: {ex.Message}");
            }
        }




        [HttpDelete("{id}")]
        public IActionResult DeleteNews(int id)
        {
            var value = _newsService.TGetByID(id);
            if (value == null)
                return NotFound("Haber bulunamadı");

            _newsService.TDelete(value);
            return Ok("Haberler Silindi");
        }

        [HttpGet("{id}")]
        public IActionResult GetNews(int id)
        {
            var value = _newsService.TGetByID(id);
            if (value == null)
                return NotFound("Haber bulunamadı");

            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateNews(UpdateNewsDto updateNewsDto)
        {
            var news = _newsService.TGetByID(updateNewsDto.NewsID);
            if (news == null)
                return NotFound("Haber bulunamadı");

            news.NewTitle = updateNewsDto.NewTitle;
            news.Description = updateNewsDto.Description;
            news.ImageUrls = updateNewsDto.ImageUrls;
            news.NewDate = updateNewsDto.NewDate;
            news.NewDetail = updateNewsDto.NewDetail;

            _newsService.TUpdate(news);
            return Ok("Haberler Güncellendi");
        }
    }
}
