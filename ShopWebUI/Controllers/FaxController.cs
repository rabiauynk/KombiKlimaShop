using Microsoft.AspNetCore.Mvc;

namespace ShopWebUI.Controllers
{
    public class FaxController:Controller    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
