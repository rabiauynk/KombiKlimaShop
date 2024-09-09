using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopWebUI.Dtos.NewsDtos;
using ShopWebUI.Dtos.NewsImageDtos;
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
                foreach (var item in values)
                {
                  var ImageResponseMessage= await client.GetAsync("https://localhost:7052/api/NewsImage/" +item.NewsID.ToString());
                    var ImageJsonData = await ImageResponseMessage.Content.ReadAsStringAsync();
                    if (ImageJsonData!= "Resimler bulunamadı")
                    {
                        var ImageValues = JsonConvert.DeserializeObject<List<ResultNewsImageDto>>(ImageJsonData);
                        item.Images = ImageValues;

                    }
                    else
                    {
                        item.Images = new List<ResultNewsImageDto>
                        {
                            new ResultNewsImageDto{ImageUrl="logo2_4594337e-9a67-4d71-a491-8006f1ceef06.png",
                            Index=0,
                            NewsID=item.NewsID
                          }
                        };
                    }

                }
                return View(values);
            }
            return View();
        }

    }
}
