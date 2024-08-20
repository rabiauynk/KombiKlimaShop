using AutoMapper;
using BusinessLayer.Abstract;
using DtoLayer.CampaignDto;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShopApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CampaignController : ControllerBase
	{
		private readonly ICampaignService _campaignService;
		private readonly IMapper _mapper;

		public CampaignController(ICampaignService campaignService, IMapper mapper)
		{
			_campaignService = campaignService;
			_mapper = mapper;
		}
		[HttpGet]
		public IActionResult CampaignList()
		{
			var value = _mapper.Map<List<ResultCampaignDto>>(_campaignService.TGetListAll());
			return Ok(value);
		}
		[HttpPost]
		public IActionResult CreateCampaign(CreateCampaignDto createCampaignDto)
		{
			_campaignService.TAdd(new Campaign()
			{
				CampaignTitle = createCampaignDto.CampaignTitle,
				Description = createCampaignDto.Description,
				ImageUrl = createCampaignDto.ImageUrl,
				CampaignDetail = createCampaignDto.CampaignDetail

			});
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
