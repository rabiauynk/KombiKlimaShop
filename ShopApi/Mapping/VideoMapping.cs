using AutoMapper;
using DtoLayer.NewsDto;
using DtoLayer.VideoDto;
using EntityLayer.Entities;

namespace ShopApi.Mapping
{
	public class VideoMapping:Profile
	{
		public VideoMapping()
		{
			CreateMap<Video,CreateVideoDto>().ReverseMap();
			CreateMap<Video,GetVideoDto>().ReverseMap();
			CreateMap<Video,ResultVideoDto>().ReverseMap();
			CreateMap<Video,UpdateVideoDto>().ReverseMap();
		}
	}
}
