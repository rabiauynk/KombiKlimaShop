using Microsoft.AspNetCore.Mvc;

namespace ShopWebUI.ViewComponents.LayoutComponent
{
    public class _LayoutHeaderComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
