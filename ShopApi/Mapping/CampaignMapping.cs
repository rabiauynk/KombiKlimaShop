using AutoMapper;
using DtoLayer.CampaignDto;
using DtoLayer.NewsDto;
using EntityLayer.Entities;

namespace ShopApi.Mapping
{
	public class CampaignMapping:Profile
	{
		public CampaignMapping()
		{
			CreateMap<Campaign, CreateCampaignDto>().ReverseMap();
			CreateMap<Campaign, GetCampaignDto>().ReverseMap();
			CreateMap<Campaign, ResultCampaignDto>().ReverseMap();
			CreateMap<Campaign, UpdateCampaignDto>().ReverseMap();
		
		}
	}
}
