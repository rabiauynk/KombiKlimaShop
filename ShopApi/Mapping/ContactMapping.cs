using AutoMapper;
using DtoLayer.ContactDto;
using DtoLayer.NewsDto;using EntityLayer.Entities;

namespace ShopApi.Mapping
{
	public class ContactMapping:Profile
	{
		public ContactMapping()
		{
			CreateMap<Contact, CreateContactDto>().ReverseMap();
			CreateMap<Contact, GetContactDto>().ReverseMap();
			CreateMap<Contact, ResultContactDto>().ReverseMap();
			CreateMap<Contact, UpdateContactDto>().ReverseMap();
		}
	}
}
