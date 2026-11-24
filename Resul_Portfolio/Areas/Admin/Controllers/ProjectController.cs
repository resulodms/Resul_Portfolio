using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;   // SelectList için
using Microsoft.EntityFrameworkCore;       // Include için
using Resul_Portfolio.Context;
using Resul_Portfolio.Entities;

namespace Resul_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly IWebHostEnvironment _env;

        // Görsel yükleme kuralları
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };
        private const long MaxFileSize = 2 * 1024 * 1024;   // 2 MB

        public ProjectController(PortfolioContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // ==================== LİSTE ====================
        public IActionResult ProjectList()
        {
            var values = _context.Projects
                                 .Include(x => x.Category)          // kategori adını göstermek için
                                 .OrderByDescending(x => x.ProjectId) // en son eklenen en üstte
                                 .ToList();
            return View(values);
        }

        // ==================== EKLE ====================
        [HttpGet]
        public IActionResult CreateProject()
        {
            LoadCategories();
            return View(new Project { IsActive = true });
        }

        [HttpPost]
        public IActionResult CreateProject(Project project, IFormFile? coverImage)
        {
            // Görsel yeni kayıtta zorunlu
            if (coverImage == null || coverImage.Length == 0)
            {
                ModelState.AddModelError("CoverImageUrl", "Lütfen bir kapak görseli seçiniz.");
            }
            else
            {
                var url = SaveImage(coverImage);
                if (url != null) project.CoverImageUrl = url;
            }

            // CoverImageUrl formdan değil bizden geliyor: [Required] kuralının otomatik kontrolünü kaldır
            ModelState.Remove("CoverImageUrl");

            // Görsel yüklenemediyse hatayı geri ekle
            if (string.IsNullOrEmpty(project.CoverImageUrl))
            {
                ModelState.AddModelError("CoverImageUrl", "Geçerli bir kapak görseli yükleyiniz.");
            }

            if (!ModelState.IsValid)
            {
                DeleteImage(project.CoverImageUrl);   // başka alanda hata varsa yüklenen görseli sil
                project.CoverImageUrl = null!;
                LoadCategories();
                return View(project);
            }

            _context.Projects.Add(project);
            _context.SaveChanges();
            TempData["Success"] = "Proje eklendi.";
            return RedirectToAction("ProjectList");
        }

        // ==================== GÜNCELLE ====================
        public IActionResult UpdateProject(int id)
        {
            var value = _context.Projects.Find(id);
            if (value == null) return NotFound();

            LoadCategories();
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateProject(Project project, IFormFile? coverImage)
        {
            // 1) Veritabanındaki mevcut kaydı çek
            var value = _context.Projects.Find(project.ProjectId);
            if (value == null) return NotFound();

            // Görsel güncellemede opsiyonel: yüklenmezse eskisi kalır
            ModelState.Remove("CoverImageUrl");

            string? newImageUrl = null;
            if (coverImage != null && coverImage.Length > 0)
            {
                newImageUrl = SaveImage(coverImage);
            }

            if (!ModelState.IsValid)
            {
                DeleteImage(newImageUrl);                      // yeni yüklenen görseli geri al
                project.CoverImageUrl = value.CoverImageUrl;   // önizlemede mevcut görsel görünsün
                LoadCategories();
                return View(project);
            }

            // 2) Formdaki alanları mevcut kayda kopyala
            value.Title = project.Title;
            value.Description = project.Description;
            value.GithubUrl = project.GithubUrl;
            value.LiveDemoUrl = project.LiveDemoUrl;
            value.Tags = project.Tags;
            value.IsActive = project.IsActive;
            value.CategoryId = project.CategoryId;

            // Yeni görsel yüklendiyse eskisini diskten sil
            if (newImageUrl != null)
            {
                DeleteImage(value.CoverImageUrl);
                value.CoverImageUrl = newImageUrl;
            }

            // 3) Kaydet (EF değişen alanları kendisi takip ediyor)
            _context.SaveChanges();
            TempData["Success"] = "Proje güncellendi.";
            return RedirectToAction("ProjectList");
        }

        // ==================== SİL ====================
        public IActionResult DeleteProject(int id)
        {
            var value = _context.Projects.Find(id);
            if (value == null) return NotFound();

            var imageUrl = value.CoverImageUrl;
            _context.Projects.Remove(value);
            _context.SaveChanges();
            DeleteImage(imageUrl);   // kayıt silindikten sonra görseli de sil

            TempData["Success"] = "Proje silindi.";
            return RedirectToAction("ProjectList");
        }

        // ==================== YARDIMCI METODLAR ====================

        // Kategori dropdown'ı için liste (form 4 farklı yerde açıldığı için tek yerde)
        private void LoadCategories()
        {
            ViewBag.Categories = new SelectList(
                _context.Categories.OrderBy(x => x.CategoryName).ToList(),
                "CategoryId",     // option value → seçilince CategoryId'ye yazılır
                "CategoryName"    // option metni → kullanıcının gördüğü
            );
        }

        // Dosyayı doğrular ve kaydeder. Başarılıysa "/images/projects/xxx.jpg" döner,
        // hatalıysa ModelState'e hata ekleyip null döner.
        private string? SaveImage(IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!AllowedExtensions.Contains(ext))
            {
                ModelState.AddModelError("CoverImageUrl", "Sadece JPG, PNG veya WEBP formatında görsel yükleyebilirsiniz.");
                return null;
            }

            if (file.Length > MaxFileSize)
            {
                ModelState.AddModelError("CoverImageUrl", "Görsel boyutu en fazla 2 MB olabilir.");
                return null;
            }

            // Benzersiz dosya adı: aynı isimli iki görsel birbirinin üzerine yazılmasın
            var fileName = $"{Guid.NewGuid()}{ext}";
            var folder = Path.Combine(_env.WebRootPath, "images", "projects");
            Directory.CreateDirectory(folder);   // klasör yoksa oluşturur, varsa bir şey yapmaz

            using (var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return $"/images/projects/{fileName}";
        }

        // Projeye ait görseli diskten siler (sadece bizim klasörümüzdekileri)
        private void DeleteImage(string? imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl) || !imageUrl.StartsWith("/images/projects/")) return;

            var path = Path.Combine(_env.WebRootPath,
                                    imageUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}