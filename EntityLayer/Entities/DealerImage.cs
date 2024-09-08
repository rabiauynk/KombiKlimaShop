using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class DealerImage
    {
        public int DealerImageID { get; set; }
        public string? ImageUrl { get; set; }
        public int Index { get; set; }
        public int DealerID { get; set; }
    }
}
