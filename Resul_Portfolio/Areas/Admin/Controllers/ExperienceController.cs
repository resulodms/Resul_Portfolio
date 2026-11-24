using Microsoft.AspNetCore.Mvc;
using Resul_Portfolio.Context;
using Resul_Portfolio.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Resul_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ExperienceController : Controller
    {
        private readonly PortfolioContext _context;

        public ExperienceController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult ExperienceList()
        {
            var values = _context.Experiences.OrderByDescending(x=>x.Duration).ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateExperience()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateExperience(Experience experience)
        {
            if (!ModelState.IsValid)
            {
                return View(experience);
            }
            _context.Experiences.Add(experience);
            _context.SaveChanges();
            TempData["Success"] = "Deneyim Bilgisi Kaydedildi";
            return RedirectToAction("ExperienceList");
        }
        public IActionResult DeleteExperience(int id)
        {
           
            var value = _context.Experiences.Find(id);
            if (value == null) return NotFound();
            _context.Experiences.Remove(value);
            _context.SaveChanges();
            TempData["Success"] = "Deneyim Bilgisi Silindi";
            return RedirectToAction("ExperienceList");
        }
        public IActionResult UpdateExperience(int id)
        {
            var value = _context.Experiences.Find(id);
            if (value == null) return NotFound();
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateExperience(Experience experience)
        {
            if (!ModelState.IsValid)
            {
                return View(experience);
            }
            _context.Experiences.Update(experience);
            _context.SaveChanges();
            TempData["Success"] = "Deneyim Bilgisi Güncellendi";
            return RedirectToAction("ExperienceList");
        }
    }
}

