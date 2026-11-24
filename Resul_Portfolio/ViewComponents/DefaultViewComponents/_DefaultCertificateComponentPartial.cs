using Microsoft.AspNetCore.Mvc;
using Resul_Portfolio.Context;

namespace Resul_Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultCertificateComponentPartial :ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultCertificateComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Certificates.Where(x=>x.IsActive).OrderByDescending(y=>y.IssueDate).ToList();
            if (!values.Any()) return Content(string.Empty);
             
            return View(values);
        }
    }
}
