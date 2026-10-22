using Microsoft.AspNetCore.Mvc;

namespace Resul_Portfolio.Areas.Admin.ViewComponents.AdminLapoutViewComponents
{
    public class _AdminLayoutSideBarComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
