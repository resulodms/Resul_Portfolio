using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resul_Portfolio.Context;

namespace Resul_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultSkillComponentPartial:ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultSkillComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var skills = _context.Skills
                        .Where(x => x.IsActive)
                        .OrderBy(x => x.DisplayOrder)
                        .ToList();

            if (!skills.Any()) return Content(string.Empty);

            // 2) Bellekte gruplara ayır
            var groups = skills
                         .GroupBy(x => x.GroupName)
                         .OrderBy(g => g.Min(x => x.DisplayOrder))
                         .ToList();

            return View(groups);
        }
    }
}
