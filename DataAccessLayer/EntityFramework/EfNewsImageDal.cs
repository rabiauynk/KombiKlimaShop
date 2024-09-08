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
    
        public class EfNewsImageDal : GenericRepository<NewsImage>, INewsImageDal
        {
            private readonly ShopContext _context;

            public EfNewsImageDal(ShopContext context) : base(context)
            {
                _context = context;
            }

            public int GetImageCountByNewsId(int newsId)
            {
                // Belirli bir newsId'ye ait kaç tane resim olduğunu sayar
                return _context.NewsImages.Count(x => x.NewsID == newsId);
            }
        }

    }

