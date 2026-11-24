using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resul_Portfolio.Context;
using Resul_Portfolio.Entities;

namespace Resul_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SkillController : Controller
    {
        private readonly PortfolioContext _context;

        public SkillController(PortfolioContext context)
        {
            _context = context;
        }
     
        public IActionResult SkillList()
        {
            var values = _context.Skills
                     .OrderBy(x => x.GroupName)
                     .ThenBy(x => x.DisplayOrder)
                     .ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateSkill()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateSkill(Skill skill)
        {
            if (!ModelState.IsValid)
            {
                return View(skill);
            }
            _context.Skills.Add(skill);
            _context.SaveChanges();
            TempData["Success"] = "Yetenek Bilgisi Kaydedildi";
            return RedirectToAction("SkillList");
        }
        public IActionResult DeleteSkill(int id)
        {

            var value = _context.Skills.Find(id);
            if (value == null) return NotFound();
            _context.Skills.Remove(value);
            _context.SaveChanges();
            TempData["Success"] = "Yetenek Bilgisi Silindi";
            return RedirectToAction("SkillList");
        }
        public IActionResult UpdateSkill(int id)
        {
            var value = _context.Skills.Find(id);
            if (value == null) return NotFound();
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateSkill(Skill skill)
        {
            if (!ModelState.IsValid)
            {
                return View(skill);
            }
            _context.Skills.Update(skill);
            _context.SaveChanges();
            TempData["Success"] = "Yetenek Bilgisi Güncellendi";
            return RedirectToAction("SkillList");
        }
        private void LoadGroups()
        {
            ViewBag.Groups = _context.Skills
                                     .Select(x => x.GroupName)
                                     .Distinct()
                                     .OrderBy(x => x)
                                     .ToList();
        }

    }
}
