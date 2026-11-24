using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resul_Portfolio.Context;
using Resul_Portfolio.Entities;

namespace Resul_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HeroSectionController : Controller
    {
        private readonly PortfolioContext _context;

        public HeroSectionController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult HeroSectionList()
        {
            var values = _context.HeroSections.ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateHeroSection()
        {
            if (_context.HeroSections.Any()) return RedirectToAction("HeroSectionList");
            return View();
        }
        [HttpPost]
        public IActionResult CreateHeroSection(HeroSection heroSection)
        {
            if (_context.HeroSections.Any()) return RedirectToAction("HeroSectionList");
            if (!ModelState.IsValid)
            {
                return View(heroSection);
            }
            _context.HeroSections.Add(heroSection);
            _context.SaveChanges();
            TempData["Success"] = "Ana Sayfa Bilgisi Kaydedildi";
            return RedirectToAction("HeroSectionList");
        }
        public IActionResult DeleteHeroSection(int id)
        {

            var value = _context.HeroSections.Find(id);
            if (value == null) return NotFound();
            _context.HeroSections.Remove(value);
            _context.SaveChanges();
            TempData["Success"] = "Ana Sayfa Bilgisi Silindi";
            return RedirectToAction("HeroSectionList");
        }
        public IActionResult UpdateHeroSection(int id)
        {
            var value = _context.HeroSections.Find(id);
            if (value == null) return NotFound();
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateHeroSection(HeroSection heroSection)
        {
            if (!ModelState.IsValid)
            {
                return View(heroSection);
            }
            _context.HeroSections.Update(heroSection);
            _context.SaveChanges();
            TempData["Success"] = "Ana Sayfa Bilgisi Güncellendi";
            return RedirectToAction("HeroSectionList");
        }
    }
}
