using Microsoft.AspNetCore.Mvc;

namespace ShopWebUI.ViewComponents.NewsComponentPartial
{
    public class _NewsComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}
