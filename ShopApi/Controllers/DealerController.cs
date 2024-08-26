using AutoMapper;
using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using DtoLayer.ContactDto;
using DtoLayer.DealerDto;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
		[HttpGet("DealerListWithCategory")]
		public IActionResult DealerListWithCategory()
		{
			var context=new ShopContext();
			var values = context.Dealers.Include(x => x.DealerCategory).Select(y => new ResultDealerWithCategory
			{
				DealerID=y.DealerID,
				DealerName = y.DealerName,
				DealerAddress = y.DealerAddress,
				DealerOwner = y.DealerOwner,
				DealerCity = y.DealerCity,
				CategoryName = y.DealerCategory.CategoryName,
				DealerDistrict = y.DealerDistrict,
				ImageUrl = y.ImageUrl,
				Phone = y.Phone,



			});
			return Ok(values.ToList());
	
		}

        [HttpPost]
        public IActionResult CreateDealer(CreateDealerDto createDealerDto)
        {
            try
            {
                _dealerService.TAdd(new Dealer
                {
                    DealerName = createDealerDto.DealerName,
                    DealerOwner = createDealerDto.DealerOwner,
                    DealerAddress = createDealerDto.DealerAddress,
                    DealerDistrict = createDealerDto.DealerDistrict,
                    DealerCity = createDealerDto.DealerCity,
                    Phone = createDealerDto.Phone,
                    ImageUrl = createDealerDto.ImageUrl,
                    DealerCategoryID = createDealerDto.DealerCategoryID
                });
                return Ok("Firma Bilgisi Eklendi");
            }
            catch (Exception ex)
            {
                // Hata loglama ve kullanıcıya bilgi verme
                return StatusCode(StatusCodes.Status500InternalServerError, $"İşlem başarısız: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
		public IActionResult DeleteDealer(int id)
		{
			var value = _dealerService.TGetByID(id);
			_dealerService.TDelete(value);
			return Ok("Firma Bilgisi Silindi ");
		}
		[HttpGet("{id}")]
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
				DealerID = updateDealerDto.DealerID,
				DealerName=updateDealerDto.DealerName,
				DealerOwner=updateDealerDto.DealerOwner,
				DealerAddress=updateDealerDto.DealerAddress ,
				DealerDistrict=updateDealerDto.DealerDistrict,
				DealerCity=updateDealerDto.DealerCity,
				Phone=updateDealerDto.Phone,
				ImageUrl=updateDealerDto.ImageUrl,
				DealerCategoryID=updateDealerDto.DealerCategoryID

			});
			return Ok("Firma Bilgisi Güncellendi");
		}


	}
}
