﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.CampaignDto
{
	public class CreateCampaignDto
	{
		public string CampaignTitle { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }
		public string CampaignDetail { get; set; }

	}
}