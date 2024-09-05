using Microsoft.AspNetCore.Mvc;

namespace ShopWebUI.ViewComponents.LayoutComponent
{
	public class _LayoutNavbarComponentPartial:ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
