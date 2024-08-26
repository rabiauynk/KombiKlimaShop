using AutoMapper;
using BusinessLayer.Abstract;
using DtoLayer.DealerCategoryDto;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShopApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DealerCategoryController : ControllerBase
	{
		private readonly IDealerCategoryService _dealerCategoryService;
		private readonly IMapper _mapper;

		public DealerCategoryController(IDealerCategoryService dealerCategoryService, IMapper mapper)
		{
			_dealerCategoryService = dealerCategoryService;
			_mapper = mapper;
		}
		[HttpGet]
		public IActionResult DealerCategoryList()
		{
			var value = _mapper.Map<List<ResultDealerCategoryDto>>(_dealerCategoryService.TGetListAll());
			return Ok(value);
		}
		[HttpPost]
		public IActionResult CreateDealerCategory(CreateDealerCategoryDto createDealerCategoryDto)
		{
			_dealerCategoryService.TAdd(new DealerCategory()
			{
				CategoryName = createDealerCategoryDto.CategoryName
			});
			return Ok("Kategori Eklendi");
		}
		[HttpDelete("{id}")]
		public IActionResult DeleteDealerCategory(int id)
		{
			var value = _dealerCategoryService.TGetByID(id);
			_dealerCategoryService.TDelete(value);
			return Ok("Kategori Silindi");
		}
		[HttpGet("{id}")]
		public IActionResult GetDealerCategory(int id)
		{
			var value = _dealerCategoryService.TGetByID(id);
			return Ok(value);
		}
		[HttpPut()]
		public IActionResult UpdateDealerCategory(UpdateDealerCategoryDto updateDealerCategoryDto)
		{
			_dealerCategoryService.TUpdate(new DealerCategory()
			{
				DealerCategoryID = updateDealerCategoryDto.DealerCategoryID,
				CategoryName = updateDealerCategoryDto.CategoryName

			});
			return Ok("Kategori Güncellendi");
		}
	}
}