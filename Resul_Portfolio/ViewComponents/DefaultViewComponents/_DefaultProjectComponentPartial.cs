using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resul_Portfolio.Context;

namespace Resul_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultProjectComponentPartial:ViewComponent
    
    {
        private readonly PortfolioContext _context;

        public _DefaultProjectComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
           var values =_context.Projects.Where(x=>x.IsActive).Include(y=>y.Category).OrderByDescending(z=>z.ProjectId).ToList();
            if (!values.Any())
            {
                return Content(string.Empty);
            }

            return View(values);
        }
    }
}
