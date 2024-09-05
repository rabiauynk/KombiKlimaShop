using Microsoft.AspNetCore.Mvc;

namespace ShopWebUI.ViewComponents.LayoutComponent
{
		public class _LayoutScriptComponentPartial : ViewComponent
		{
			public IViewComponentResult Invoke()
			{
				return View();
			}
		}
}
