using Microsoft.AspNetCore.Mvc;

namespace Resul_Portfolio.Areas.Admin.ViewComponents.AdminLapoutViewComponents
{
    public class _AdminLayoutStyleComponentPartial :ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
