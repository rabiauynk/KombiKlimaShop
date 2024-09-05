using Microsoft.AspNetCore.Mvc;

namespace ShopWebUI.ViewComponents.LayoutComponent
{
	public class _LayoutSidebarComponentPartial : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
