using Microsoft.AspNetCore.Mvc;

namespace Resul_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultStyleComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
