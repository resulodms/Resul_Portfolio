using Microsoft.AspNetCore.Mvc;
using Resul_Portfolio.Context;

namespace Resul_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultSocialLinksComponentPartial:ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultSocialLinksComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public  IViewComponentResult Invoke()
        {
            var value= _context.Contacts.FirstOrDefault();
            if (value == null)
            {
                return Content(string.Empty);
            }
            return View(value);
        }
    }
}
