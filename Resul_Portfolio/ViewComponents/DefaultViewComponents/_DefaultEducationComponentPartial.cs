using Microsoft.AspNetCore.Mvc;
using Resul_Portfolio.Context;

namespace Resul_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultEducationComponentPartial:ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultEducationComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Educations.Where(x => x.IsActive).OrderByDescending(x => x.Duration).ToList();
            if (!values.Any())
            {
                return Content(string.Empty);
            }
            return View(values);
        }
    }
}
