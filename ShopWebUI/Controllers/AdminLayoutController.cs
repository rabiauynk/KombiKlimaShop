using Microsoft.AspNetCore.Mvc;

namespace ShopWebUI.Controllers
{
	public class AdminLayoutController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
