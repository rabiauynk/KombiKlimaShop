using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
	public class DealerCategory
	{
		public int DealerCategoryID { get; set; }
		public string CategoryName { get; set; }
		public List<Dealer> Dealers { get; set; }
	}
}
