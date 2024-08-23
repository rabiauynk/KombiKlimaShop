using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopWebUI.Dtos.DealerCategoryDtos;

namespace ShopWebUI.Controllers
{
	public class DealerCategoryController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public DealerCategoryController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> Index()
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7052/api/DealerCategory");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultDealerCategoryDto>>(jsonData);
				return View(values);
			}
			return View();
		}
		[HttpGet]
		public IActionResult CreateCategory()
		{
			return View();
		}
		[HttpPost]
		public async  Task<IActionResult> CreateCategory() {
}
