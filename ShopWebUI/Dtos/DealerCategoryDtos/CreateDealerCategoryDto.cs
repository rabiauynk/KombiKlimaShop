using System.ComponentModel.DataAnnotations;

namespace ShopWebUI.Dtos.DealerCategoryDtos
{
	public class CreateDealerCategoryDto
	{
        [Required(ErrorMessage = "Kategori adý gereklidir.")]
        public string CategoryName { get; set; }
	}
}
