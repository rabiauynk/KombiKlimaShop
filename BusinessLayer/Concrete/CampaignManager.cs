using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class CampaignManager:ICampaignService
	{
		private readonly ICampaignDal _campaignDal;

		public CampaignManager(ICampaignDal campaignDal)
		{
			_campaignDal = campaignDal;
		}

		public void TAdd(Campaign entity)
		{
		    _campaignDal.Add(entity);
		}

		public void TDelete(Campaign entity)
		{
			_campaignDal.Delete(entity);
		}

		public void TUpdate(Campaign entity)
		{
			_campaignDal.Update(entity);
		}

		public Campaign TGetByID(int id)
		{
			return _campaignDal.GetByID(id);
		}

		public List<Campaign> TGetListAll()
		{
			return _campaignDal.GetListAll();
		}

	}
}
