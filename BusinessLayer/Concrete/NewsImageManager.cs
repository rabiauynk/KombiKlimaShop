﻿using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class NewsImageManager : INewsImageService
    {
        private readonly INewsImageDal _newsImageDal;

        public NewsImageManager(INewsImageDal newsImageDal)
        {
            _newsImageDal = newsImageDal;
        }
        public void TAdd(NewsImage entity)
         {
                int imageCount = _newsImageDal.GetImageCountByNewsId(entity.NewsID);

                if (imageCount >= 5)
                {
                    throw new Exception("Bir habere en fazla 5 resim eklenebilir.");
                }

                _newsImageDal.Add(entity);
         }

        

        public void TDelete(NewsImage entity)
        {
             _newsImageDal.Delete(entity);
        }

        public NewsImage TGetByID(int id)
        {
            return _newsImageDal.GetByID(id);
        }

        public int TGetImageCountByNewsId(int newsId)
        {
            // Veri katmanındaki metodu çağırıyoruz
            return _newsImageDal.GetImageCountByNewsId(newsId);
        }

        public List<NewsImage> TGetListAll()
        {
            return _newsImageDal.GetListAll();
        }

        public void TUpdate(NewsImage entity)
        {
           _newsImageDal.Update(entity);
        }
     

    }
}
