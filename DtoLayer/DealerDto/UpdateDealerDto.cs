using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.DealerDto
{
	public class UpdateDealerDto
	{
		public int DealerID { get; set; }
		public string DealerName { get; set; }
		public string DealerOwner { get; set; }
		public string DealerAddress { get; set; }
		public string DealerDistrict { get; set; }
		public string DealerCity { get; set; }
		public string Phone { get; set; }
		public string ImageUrl { get; set; }

	}
}
