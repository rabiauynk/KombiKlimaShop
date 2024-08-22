using Microsoft.AspNetCore.Mvc;

namespace ShopWebUI.Controllers
{
	public class DealerCategoryController : Controller
	{
		private readonly	IHttpClientFactory httpClientFactory;

		public DealerCategoryController(IHttpClientFactory httpClientFactory)
		{
			this.httpClientFactory = httpClientFactory;
		}

		public async Task <IActionResult> Index()
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage=await client 
			return View();
		}
	}
}
