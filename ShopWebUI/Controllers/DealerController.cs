using Microsoft.AspNetCore.Mvc;

namespace ShopWebUI.Controllers
{
    public class DealerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
