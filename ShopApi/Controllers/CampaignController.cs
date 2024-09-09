using AutoMapper;
using BusinessLayer.Abstract;
using DtoLayer.CampaignDto;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace ShopApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CampaignController : ControllerBase
	{
		private readonly ICampaignService _campaignService;
		private readonly IMapper _mapper;
	    private readonly string _uiRootPath;

        public CampaignController(ICampaignService campaignService, IMapper mapper, IConfiguration configuration)
        {
            _campaignService = campaignService;
            _mapper = mapper;
            _uiRootPath = configuration["UIRootPath"];
        
        }

        [HttpGet]
		public IActionResult CampaignList()
		{
			var value = _mapper.Map<List<ResultCampaignDto>>(_campaignService.TGetListAll());
			return Ok(value);
		}
        [HttpPost]
        public async Task<IActionResult> CreateCampaign(string campaignTitle, string description, string campaignDetail, IFormFile file)
        {
            // Dosya kontrolü
            if (file == null || file.Length == 0)
                return BadRequest("Dosya seçilmedi");

            // Dosya uzantısı kontrolü
            var permittedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                return BadRequest("Yalnızca görüntü dosyaları kabul edilir (jpg, jpeg, png, gif).");

            // UI kök yolu kontrolü
            if (string.IsNullOrEmpty(_uiRootPath))
                return StatusCode(StatusCodes.Status500InternalServerError, "UIRootPath is not configured.");

            var uploadPath = Path.Combine(_uiRootPath, "campaign-images");

            // Dosya yükleme dizini oluşturma
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

            // Dosyayı kaydetme
            try
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Dosya kaydedilemedi: {ex.Message}");
            }

         

            // Kampanya oluşturma
            try
            {
                var campaign = new Campaign
                {
                    CampaignTitle = campaignTitle,
                    Description = description,
                    ImageUrl = uniqueFileName,
                    CampaignDetail = campaignDetail
                };

            _campaignService.TAdd(campaign);
        }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Kampanya eklenemedi: {ex.Message}");
            }

            return Ok("Kampanya Eklendi");
        }

        [HttpDelete]
		public IActionResult DeleteCampaign(int id)
		{
			var value = _campaignService.TGetByID(id);
			_campaignService.TDelete(value);
			return Ok("Kampanya Silindi");
		}
		[HttpGet("GetCampaign")]
		public IActionResult GetCampaign(int id)
		{
			var value = _campaignService.TGetByID(id);
			return Ok(value);
		}
        [HttpPost("{campaignId}/upload")]
        public async Task<IActionResult> UploadCampaignImage(int campaignId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Dosya seçilmedi");

            var permittedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                return BadRequest("Yalnızca görüntü dosyaları kabul edilir (jpg, jpeg, png, gif).");

            if (string.IsNullOrEmpty(_uiRootPath))
                return StatusCode(StatusCodes.Status500InternalServerError, "UIRootPath is not configured.");

            var uploadPath = Path.Combine(_uiRootPath, "campaign-images");

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

                // Kampanya görselini güncelle
                var campaign = _campaignService.TGetByID(campaignId);
                if (campaign == null)
                    return NotFound("Kampanya bulunamadı");

                campaign.ImageUrl = uniqueFileName;
                _campaignService.TUpdate(campaign);

                return Ok(new { imageUrl = uniqueFileName });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"İşlem başarısız: {ex.Message}");
            }
        }

        [HttpPut]
		public IActionResult UpdateCampaign(UpdateCampaignDto updateCampaignDto)
		{
			_campaignService.TUpdate(new Campaign()
			{
				CampaignID = updateCampaignDto.CampaignID,
				CampaignTitle = updateCampaignDto.CampaignTitle,
				Description = updateCampaignDto.Description,
				ImageUrl = updateCampaignDto.ImageUrl,
				CampaignDetail = updateCampaignDto.CampaignDetail

			});
			return Ok("Kampanya GÜncellendi");
		}
	}
}
