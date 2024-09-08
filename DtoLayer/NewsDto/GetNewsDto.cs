using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.NewsDto
{
	public class GetNewsDto
	{
		public int NewsID { get; set; }
		public string NewTitle { get; set; }
		public string Description { get; set; }
        
        public DateTime NewDate { get; set; }
		public string NewDetail { get; set; }
		
	}
}
