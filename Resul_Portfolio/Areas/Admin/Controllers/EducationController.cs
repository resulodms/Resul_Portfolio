using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resul_Portfolio.Context;
using Resul_Portfolio.Entities;

namespace Resul_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class EducationController : Controller
    {
        private readonly PortfolioContext _context;

        public EducationController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult EducationList()
        {
            var values = _context.Educations.OrderByDescending(x => x.Duration).ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateEducation()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateEducation(Education education)
        {
            if (!ModelState.IsValid)
            {
                return View(education);
            }
            _context.Educations.Add(education);
            _context.SaveChanges();
            TempData["Success"] = "Eğitim Bilgisi Kaydedildi";
            return RedirectToAction("EducationList");
        }
        public IActionResult DeleteEducation(int id)
        {

            var value = _context.Educations.Find(id);
            if (value == null) return NotFound();
            _context.Educations.Remove(value);
            _context.SaveChanges();
            TempData["Success"] = "Eğitim Bilgisi Silindi";
            return RedirectToAction("EducationList");
        }
        public IActionResult UpdateEducation(int id)
        {
            var value = _context.Educations.Find(id);
            if (value == null) return NotFound();
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateEducation(Education education)
        {
            if (!ModelState.IsValid)
            {     
                return View(education);
            }
            _context.Educations.Update(education);
            _context.SaveChanges();
            TempData["Success"] = "Eğitim Bilgisi Güncellendi";
            return RedirectToAction("EducationList");
        }
    }
}
