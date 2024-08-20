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
	public class VideoManager : IVideoService
	{
		private readonly IVideoDal _videoDal;

		public VideoManager(IVideoDal videoDal)
		{
			_videoDal = videoDal;
		}

		public void TAdd(Video entity)
		{
			_videoDal.Add(entity);
		}

		public void TDelete(Video entity)
		{
			_videoDal.Delete(entity);
		}

		public Video TGetByID(int id)
		{
			return _videoDal.GetByID(id);
		}

		public List<Video> TGetListAll()
		{
			return _videoDal.GetListAll();
		}

		public void TUpdate(Video entity)
		{
			_videoDal.Update(entity);
		}
	}
}
