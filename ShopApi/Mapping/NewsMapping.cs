using AutoMapper;
using DtoLayer.NewsDto;
using EntityLayer.Entities;


namespace ShopApi.Mapping
{
	public class NewsMapping : Profile
	{
		public NewsMapping()
		{
			CreateMap<News,CreateNewsDto>().ReverseMap();
			CreateMap<News,GetNewsDto>().ReverseMap();
			CreateMap<News,ResultNewsDto>().ReverseMap();
			CreateMap<News,UpdateNewsDto>().ReverseMap();
		}
	}
}
