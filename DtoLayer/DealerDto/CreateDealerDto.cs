using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.DealerDto
{
	public class CreateDealerDto
	{
		public string DealerName { get; set; }
		public string Authorized { get; set; }
		public string Location { get; set; }
		public string Phone { get; set; }
	}
}
