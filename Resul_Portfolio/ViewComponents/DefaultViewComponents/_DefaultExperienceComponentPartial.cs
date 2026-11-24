using Microsoft.AspNetCore.Mvc;
using Resul_Portfolio.Context;

namespace Resul_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultExperienceComponentPartial:ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultExperienceComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values= _context.Experiences.Where(x=>x.IsActive).OrderByDescending(x=>x.Duration).ToList();
            if (!values.Any())
            {
                return  Content(string.Empty);
            }
            return View(values);
        }
    }
}
