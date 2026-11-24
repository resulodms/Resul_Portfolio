using Microsoft.AspNetCore.Mvc;
using Resul_Portfolio.Context;

namespace Resul_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultAboutComponentPartial:ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultAboutComponentPartial(PortfolioContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var values = _context.Abouts.FirstOrDefault();
            if (values == null)
            {
                return Content(string.Empty);
            }
            return View(values);
        }
    }
}
