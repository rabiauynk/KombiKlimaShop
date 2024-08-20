﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.NewsDto
{
	public class UpdateNewsDto
	{
		public int NewsID { get; set; }
		public string NewTitle { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }
		public DateTime NewDate { get; set; }
		public string NewDetail { get; set; }
	
	}
}