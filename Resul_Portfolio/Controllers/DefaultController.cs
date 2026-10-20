using Microsoft.AspNetCore.Mvc;

namespace Resul_Portfolio.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
