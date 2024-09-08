using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopWebUI.Dtos.CampaignDtos;

namespace ShopWebUI.Controllers
{
    public class CampaignController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CampaignController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7052/api/Campaign");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCampaignDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
