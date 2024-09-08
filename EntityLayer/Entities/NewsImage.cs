using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class NewsImage
    {
        public int NewsImageID { get; set; }
        public string? ImageUrl { get; set; }
        public int Index { get; set; }
        public int NewsID { get; set; }
    }
}
