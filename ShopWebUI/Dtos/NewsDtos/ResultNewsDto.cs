using ShopWebUI.Dtos.NewsImageDtos;

namespace ShopWebUI.Dtos.NewsDtos{
    public class ResultNewsDto
    {
       
		public int NewsID { get; set; }
        public string NewTitle { get; set; }
        public string Description { get; set; }
     
        public DateTime NewDate { get; set; }
        public string NewDetail { get; set; }
        public List<ResultDealerImageDto> Images { get; set; }
    }
}
