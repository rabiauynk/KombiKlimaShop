using System.ComponentModel.DataAnnotations;

namespace ShopWebUI.Dtos.DealerCategoryDtos
{
	public class CreateDealerCategoryDto
	{
        [Required(ErrorMessage = "Kategori ad� gereklidir.")]
        public string CategoryName { get; set; }
	}
}
