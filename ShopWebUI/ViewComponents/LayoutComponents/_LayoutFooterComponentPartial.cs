using Microsoft.AspNetCore.Mvc;

namespace ShopWebUI.ViewComponents.LayoutComponent
{
	public class _LayoutFooterComponentPartial : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
