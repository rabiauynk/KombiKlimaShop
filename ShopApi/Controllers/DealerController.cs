using AutoMapper;
using BusinessLayer.Abstract;
using DtoLayer.ContactDto;
using DtoLayer.DealerDto;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShopApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DealerController : ControllerBase
	{
		private readonly IDealerService _dealerService;
		private readonly IMapper _mapper;

		public DealerController(IDealerService dealerService, IMapper mapper)
		{
			_dealerService = dealerService;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult DealerList()
		{
			var value = _mapper.Map<List<ResultDealerDto>>(_dealerService.TGetListAll());
			return Ok(value);
		}
		[HttpPost]
		public IActionResult CreateDealer(CreateDealerDto createDealerDto)
		{
			_dealerService.TAdd(new ()
			{
				DealerName = createDealerDto.DealerName,
				Authorized=createDealerDto.Authorized,
				Location=createDealerDto.Location

			});
			return Ok("Bayi Bilgisi Eklendi");
		}
		[HttpDelete]
		public IActionResult DeleteDealer(int id)
		{
			var value = _dealerService.TGetByID(id);
			_dealerService.TDelete(value);
			return Ok("Bayi Bilgisi Silindi ");
		}
		[HttpGet("GetDealer")]
		public IActionResult GetDealer(int id)
		{
			var value = _dealerService.TGetByID(id);
			return Ok(value);
		}
		[HttpPut]
		public IActionResult UpdateDealer(UpdateDealerDto updateDealerDto)
		{
			_dealerService.TUpdate(new Dealer()
			{
				DealerID=updateDealerDto.DealerID,
				DealerName=updateDealerDto.DealerName,
				Authorized=updateDealerDto.Authorized,
				Location=updateDealerDto.Location,
				Phone=updateDealerDto.Phone
			});
			return Ok("Bayi Bilgisi Güncellendi");
		}


	}
}
