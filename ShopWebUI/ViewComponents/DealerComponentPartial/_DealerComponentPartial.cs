using Microsoft.AspNetCore.Mvc;

namespace ShopWebUI.ViewComponents.DealerComponentPartial
{
    public class _DealerComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
