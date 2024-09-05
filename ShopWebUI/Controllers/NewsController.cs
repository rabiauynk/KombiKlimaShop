using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopWebUI.Dtos.NewsDtos;
using ShopWebUI.Dtos.VideoDtos;
using System.Net.Http;

namespace ShopWebUI.Controllers
{
    public class NewsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public NewsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7052/api/News");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultNewsDto>>(jsonData);
                return View(values);
            }
            return View();
        }

    }
}
