using AutoMapper;
using DtoLayer.DealerCategoryDto;
using DtoLayer.NewsDto;
using EntityLayer.Entities;

namespace ShopApi.Mapping
{
	public class DealerCategoryMapping:Profile
	{
		public DealerCategoryMapping()
		{
			CreateMap<DealerCategory, CreateDealerCategoryDto>().ReverseMap();
			CreateMap<DealerCategory, GetDealerCategoryDto>().ReverseMap();
			CreateMap<DealerCategory, ResultDealerCategoryDto>().ReverseMap();
			CreateMap<DealerCategory, UpdateDealerCategoryDto>().ReverseMap();
		}
	}
}
