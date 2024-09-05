using Microsoft.AspNetCore.Mvc;

namespace ShopWebUI.ViewComponents.UILayoutComponents
{
    public class _UILayoutTopbarComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
