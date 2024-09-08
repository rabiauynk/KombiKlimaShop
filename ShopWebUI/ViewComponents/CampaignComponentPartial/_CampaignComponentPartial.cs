using Microsoft.AspNetCore.Mvc;

namespace ShopWebUI.ViewComponents.CampaignComponentPartial
{
    public class _CampaignComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
