using Microsoft.AspNetCore.Mvc;
using Resul_Portfolio.Context;
using Resul_Portfolio.Entities;
using Resul_Portfolio.Models;

namespace Resul_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultHeroSectionComponentPartial:ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultHeroSectionComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.HeroSections.FirstOrDefault();
            if (values ==null)
            {
                return Content(string.Empty);
            }
            var about =  _context.Abouts.FirstOrDefault();
            var viewModel = new HeroViewModel
            {
                Hero = values,
                JobTitle = about?.JobTitle,
                Expert = about?.Expert
            };

            return View(viewModel);
        }
    }
}
