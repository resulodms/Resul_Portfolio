using Microsoft.AspNetCore.Mvc;
using Resul_Portfolio.Context;
using Resul_Portfolio.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Resul_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AboutController : Controller
    {
        private readonly PortfolioContext _context;

        public AboutController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult AboutList()
        {
            var values = _context.Abouts.ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateAbout()
        {
            if (_context.Abouts.Any()) return RedirectToAction("AboutList");
            return View();
        }
        [HttpPost]
        public IActionResult CreateAbout(About about)
        {
            if (_context.Abouts.Any()) return RedirectToAction("AboutList");
            if (!ModelState.IsValid)
            {
                return View(about);
            }
            _context.Abouts.Add(about);
            _context.SaveChanges();
            TempData["Success"] = "Hakkımda Bilgisi Kaydedildi";
            return RedirectToAction("AboutList");
        }
        public IActionResult DeleteAbout(int id)
        {

            var value = _context.Abouts.Find(id);
            if (value == null) return NotFound();
            _context.Abouts.Remove(value);
            _context.SaveChanges();
            TempData["Success"] = "Hakkımda Bilgisi Silindi";
            return RedirectToAction("AboutList");
        }
        public IActionResult UpdateAbout(int id)
        {
            var value = _context.Abouts.Find(id);
            if (value == null) return NotFound();
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateAbout(About about)
        {
            if (!ModelState.IsValid)
            {
                return View(about);
            }
            _context.Abouts.Update(about);
            _context.SaveChanges();
            TempData["Success"] = "Hakkımda Bilgisi Güncellendi";
            return RedirectToAction("AboutList");
        }
    }
}
