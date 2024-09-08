using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IDealerImageDal : IGenericDal<DealerImage>
    {
        int GetImageCountByDealerId(int dealerId);

    }
}
