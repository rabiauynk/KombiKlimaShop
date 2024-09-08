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
    public class EfDealerImageDal : GenericRepository<DealerImage>, IDealerImageDal
    {

        private readonly ShopContext _context;

        public EfDealerImageDal(ShopContext context) : base(context)
        {
            _context = context;
        }

        public int GetImageCountByDealerId(int dealerId)
        {
            // Belirli bir newsId'ye ait kaç tane resim olduğunu sayar
            return _context.DealerImages.Count(x => x.DealerID == dealerId);
        }
    }
}
