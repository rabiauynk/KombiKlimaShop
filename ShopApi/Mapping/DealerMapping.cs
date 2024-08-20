using AutoMapper;
using DtoLayer.DealerDto;
using DtoLayer.NewsDto;
using EntityLayer.Entities;

namespace ShopApi.Mapping
{
	public class DealerMapping:Profile
	{
		public DealerMapping()
		{
			CreateMap<Dealer,CreateDealerDto>().ReverseMap();
			CreateMap<Dealer,GetDealerDto>().ReverseMap();
			CreateMap<Dealer,ResultDealerDto>().ReverseMap();
			CreateMap<Dealer,UpdateDealerDto>().ReverseMap();

		}
	}
}
