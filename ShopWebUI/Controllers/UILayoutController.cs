using Microsoft.AspNetCore.Mvc;

namespace ShopWebUI.Controllers
{
    public class UILayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
