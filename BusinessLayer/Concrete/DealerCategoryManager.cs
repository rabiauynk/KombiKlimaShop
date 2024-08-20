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
	public class DealerCategoryManager : IDealerCategoryService
	{
		private readonly IDealerCategoryDal _dealerCategoryDal;

		public DealerCategoryManager(IDealerCategoryDal dealerCategoryDal)
		{
			_dealerCategoryDal = dealerCategoryDal;
		}

		public void TAdd(DealerCategory entity)
		{
			_dealerCategoryDal.Add(entity);
		}

		public void TDelete(DealerCategory entity)
		{
			_dealerCategoryDal.Delete(entity);
		}

		public DealerCategory TGetByID(int id)
		{
			return _dealerCategoryDal.GetByID(id);
		}

		public List<DealerCategory> TGetListAll()
		{
			return _dealerCategoryDal.GetListAll();
		}

		public void TUpdate(DealerCategory entity)
		{
			_dealerCategoryDal.Update(entity);
		}
	}
}
