using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
	public class GenericRepository<T> : IGenericDal<T> where T : class
	{
		private readonly ShopContext _context;

		public GenericRepository(ShopContext context)
		{
			_context = context;
		}

		public void Add(T entity)
		{
			_context.Add(entity);
			_context.SaveChanges();
		}

		public void Delete(T entity)
		{
			_context.Remove(entity);
			_context.SaveChanges();
		}

		public T GetByID(int id)
		{
			return _context.Set<T>().Find(id);
		}

		public List<T> GetListAll()
		{
			return _context.Set<T>().ToList();
		}

		public void Update(T entity)
		{
			_context.Update(entity);
			_context.SaveChanges();
		}
        public void UpdateDealerImages(int dealerId, List<string> imageUrls)
        {
            var dealer = _context.Dealers.Find(dealerId);
            if (dealer == null) throw new ArgumentException("Dealer not found");

            dealer.ImageUrls = imageUrls;
            _context.Update(dealer);
            _context.SaveChanges();
        }
        public void UpdateNewsImages(int newsId, List<string> imageUrls)
        {
            var news = _context.New.Find(newsId);
            if (news == null) throw new ArgumentException("News not found");

            news.ImageUrls = imageUrls;
            _context.Update(news);
            _context.SaveChanges();
        }


    }
}
