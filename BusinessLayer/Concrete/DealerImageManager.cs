using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class DealerImageManager : IDealerImageService
    {
        private readonly IDealerImageDal _dealerImageDal;

        public DealerImageManager(IDealerImageDal dealerImageDal)
        {
            _dealerImageDal = dealerImageDal;
        }

        public void TAdd(DealerImage entity)
        {
            int imageCount = _dealerImageDal.GetImageCountByDealerId(entity.DealerID);

            if (imageCount >= 5)
            {
                throw new Exception("Bir habere en fazla 5 resim eklenebilir.");
            }

            _dealerImageDal.Add(entity);
        }

        public void TDelete(DealerImage entity)
        {
            _dealerImageDal.Delete(entity);
        }

        public DealerImage TGetByID(int id)
        {
            return _dealerImageDal.GetByID(id);
        }

        public int TGetImageCountByImageId(int dealerId)
        {
            return _dealerImageDal.GetImageCountByDealerId(dealerId);
        }

        public List<DealerImage> TGetListAll()
        {
            return _dealerImageDal.GetListAll();
        }

        public void TUpdate(DealerImage entity)
        {
            _dealerImageDal.Update(entity);
        }
    }
}
