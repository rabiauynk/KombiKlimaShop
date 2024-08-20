using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
	public class EfDealerCategoryDal : GenericRepository<DealerCategory>, IDealerCategoryDal
	{
		public EfDealerCategoryDal(ShopContext context) : base(context)
		{
		}
	}
}
