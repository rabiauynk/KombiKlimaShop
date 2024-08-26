using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
	public class EfDealerDal : GenericRepository<Dealer>, IDealerDal
	{
		public EfDealerDal(ShopContext context) : base(context)
		{
		}

		public List<Dealer> GetDealersWithCategories()
		{
			var context=new ShopContext();
			var values=context.Dealers.Include(x=>x.DealerCategory).ToList();
			return values;
		}
	}
}
