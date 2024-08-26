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
	public class DealerManager : IDealerService
	{
		private readonly IDealerDal _dealerDal;

		public DealerManager(IDealerDal dealerDal)
		{
			_dealerDal = dealerDal;
		}

		public void TAdd(Dealer entity)
		{
			_dealerDal.Add(entity);
		}

		public void TDelete(Dealer entity)
		{
		    _dealerDal.Delete(entity);
		}

		public Dealer TGetByID(int id)
		{
			return _dealerDal.GetByID(id);
		}

		public List<Dealer> TGetDealersWithCategories()
		{
			return _dealerDal.GetDealersWithCategories();
		}

		public List<Dealer> TGetListAll()
		{
			return _dealerDal.GetListAll();
		}
		public void TUpdate(Dealer entity)
		{
			_dealerDal.Update(entity);
		}

	}
}
