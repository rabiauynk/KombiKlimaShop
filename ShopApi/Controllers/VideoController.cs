using AutoMapper;
using BusinessLayer.Abstract;
using DtoLayer.NewsDto;
using DtoLayer.VideoDto;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShopApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VideoController : ControllerBase
	{
		private readonly IVideoService _videoService;
		private readonly IMapper _mapper;

		public VideoController(IVideoService videoService, IMapper mapper)
		{
			_videoService = videoService;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult VideoList()
		{
			var value = _mapper.Map<List<ResultVideoDto>>(_videoService.TGetListAll());
			return Ok(value);
		}
		[HttpPost]
		public IActionResult CreateNews(CreateVideoDto createVideoDto)
		{
			_videoService.TAdd(new()
			{
				VideoUrl = createVideoDto.VideoUrl
			});
			return Ok("Video Eklendi");
		}
		[HttpDelete]
		public IActionResult DeleteVideo(int id)
		{
			var value = _videoService.TGetByID(id);
			_videoService.TDelete(value);
			return Ok("Video Silindi ");
		}
		[HttpGet("GetVideo")]
		public IActionResult GetVideo(int id)
		{
			
			var value = _videoService.TGetByID(id);
			return Ok(value);
		}
		[HttpPut]
		public IActionResult UpdateVideo(UpdateVideoDto updateVideoDto)
		{
			_videoService.TUpdate(new Video()
			{
				VideoID = updateVideoDto.VideoID,
				VideoUrl=updateVideoDto.VideoUrl

			});
			return Ok("Video Güncellendi");
		}


	}
}
