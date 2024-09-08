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
                NewDate = createNewsDto.NewDate,
                NewDetail = createNewsDto.NewDetail
            };

            _newsService.TAdd(news);
            return Ok("Haberler Eklendi");
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
         
            news.NewDate = updateNewsDto.NewDate;
            news.NewDetail = updateNewsDto.NewDetail;

            _newsService.TUpdate(news);
            return Ok("Haberler Güncellendi");
        }
    }
}
