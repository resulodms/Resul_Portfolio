using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resul_Portfolio.Context;
using Resul_Portfolio.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Resul_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly PortfolioContext _context;

        public CategoryController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult CategoryList()
        {
            var values = _context.Categories.Include(x=>x.Projects).OrderBy(y=>y.CategoryName).ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            _context.Categories.Add(category);
            _context.SaveChanges();
            TempData["Success"] = "Kategori Bilgisi Kaydedildi";
            return RedirectToAction("CategoryList");
        }
        public IActionResult DeleteCategory(int id)
        {

            var value = _context.Categories.Find(id);
            if (value == null) return NotFound();
            var projectCount = _context.Projects.Count(x=>x.CategoryId == id);
            if (projectCount > 0)
            {
                TempData["Error"] = $"Bu kategoride {projectCount} proje var. Önce projeleri başka bir kategoriye taşıyın veya silin.";
                return RedirectToAction("CategoryList");
            }
            _context.Categories.Remove(value);
            _context.SaveChanges();
            TempData["Success"] = "Kategori Bilgisi Silindi";
            return RedirectToAction("CategoryList");
        }
        public IActionResult UpdateCategory(int id)
        {
            var value = _context.Categories.Find(id);
            if (value == null) return NotFound();
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            _context.Categories.Update(category);
            _context.SaveChanges();
            TempData["Success"] = "Kategori Bilgisi Güncellendi";
            return RedirectToAction("CategoryList");
        }
    }
}
