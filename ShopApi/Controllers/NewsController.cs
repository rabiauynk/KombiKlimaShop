using AutoMapper;
using BusinessLayer.Abstract;
using DtoLayer.ContactDto;
using DtoLayer.DealerDto;
using DtoLayer.NewsDto;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShopApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NewsController : ControllerBase
	{
		private readonly INewsService _newsService;
		private readonly IMapper _mapper;

		public NewsController(INewsService newsService, IMapper mapper)
		{
			_newsService = newsService;
			_mapper = mapper;
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
			_newsService.TAdd(new()
			{
				NewTitle = createNewsDto.NewTitle,
				Description = createNewsDto.Description,
				ImageUrl = createNewsDto.ImageUrl,
				NewDate = createNewsDto.NewDate,
				NewDetail = createNewsDto.NewDetail
				


			});
			return Ok("Haberler Eklendi");
		}
		[HttpDelete]
		public IActionResult DeleteNews(int id)
		{
			var value = _newsService.TGetByID(id);
			_newsService.TDelete(value);
			return Ok("Haberler Silindi ");
		}
		[HttpGet("GetNews")]
		public IActionResult GetNews(int id)
		{
			var value = _newsService.TGetByID(id);
			return Ok(value);
		}
		[HttpPut]
		public IActionResult UpdateNews(UpdateNewsDto updateNewsDto)
		{
			_newsService.TUpdate(new News()
			{
				NewsID = updateNewsDto.NewsID,
				NewTitle = updateNewsDto.NewTitle,	
				Description = updateNewsDto.Description,
				ImageUrl = updateNewsDto.ImageUrl,
				NewDate = updateNewsDto.NewDate,
				NewDetail = updateNewsDto.NewDetail
				
			});
			return Ok("Haberler Güncellendi");
		}


	}
}

