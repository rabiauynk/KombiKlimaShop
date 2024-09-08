using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
	public class News
	{
        public int NewsID { get; set; }
		public string NewTitle { get; set; }
		public string Description { get; set; }
        public DateTime NewDate { get; set; }
		public string NewDetail { get; set; }
    }
}
