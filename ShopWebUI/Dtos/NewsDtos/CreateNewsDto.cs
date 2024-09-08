namespace ShopWebUI.Dtos.NewsDtos
{
	public class CreateNewsDto
    {
	
        public string NewTitle { get; set; }
        public string Description { get; set; }

        public DateTime NewDate { get; set; }
        public string NewDetail { get; set; }
    }
}
