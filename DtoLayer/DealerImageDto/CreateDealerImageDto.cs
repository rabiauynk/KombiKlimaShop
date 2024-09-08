using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.DealerImageDto
{
    public class CreateDealerImageDto
    {
        public string? ImageUrl { get; set; }
        public int Index { get; set; }
        public int DealerID { get; set; }
    }
}
