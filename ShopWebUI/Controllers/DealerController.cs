using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopWebUI.Dtos.DealerDtos;
using ShopWebUI.Dtos.DealerImageDtos;
using System.Net.Http;

namespace ShopWebUI.Controllers
{
    public class DealerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DealerController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7052/api/Dealer");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultDealerDto>>(jsonData);
                foreach (var item in values)
                {
                    var imageResponseMessage = await client.GetAsync("https://localhost:7052/api/DealerImage/" + item.DealerID.ToString());
                    var imageJsonData = await imageResponseMessage.Content.ReadAsStringAsync();
                    if (imageJsonData != "Resimler bulunamadı")
                    {
                        var imageValues = JsonConvert.DeserializeObject<List<ResultDealerImageDto>>(imageJsonData);
                        item.Images = imageValues;
                    }
                    else
                    {
                        item.Images = new List<ResultDealerImageDto>
                {
                    new ResultDealerImageDto { ImageUrl = "default-dealer.png", Index = 0, DealerID = item.DealerID }
                };
                    }
                }

                return View(values);
            }

            return View(new List<ResultDealerDto>()); // Boş bir liste döndürüyoruz
        }

    }
}
