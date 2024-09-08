using Microsoft.AspNetCore.Mvc;

namespace ShopWebUI.ViewComponents.VideoComponentPartial
{
    public class _VideoComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
