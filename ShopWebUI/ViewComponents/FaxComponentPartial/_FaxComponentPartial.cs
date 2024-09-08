using Microsoft.AspNetCore.Mvc;

namespace ShopWebUI.ViewComponents.SSSComponentPartial
{
    public class _FaxComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
