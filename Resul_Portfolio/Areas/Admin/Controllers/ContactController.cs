using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resul_Portfolio.Context;
using Resul_Portfolio.Entities;

namespace Resul_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ContactController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly IWebHostEnvironment _env;

        public ContactController(PortfolioContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult ContactList()
        {
            var values = _context.Contacts.ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateContact()
        {
            if (_context.Contacts.Any()) return RedirectToAction("ContactList");
            return View();
        }
        [HttpPost]
        public IActionResult CreateContact(Contact contact)
        {
            if (_context.Contacts.Any()) return RedirectToAction("ContactList");
            if (!ModelState.IsValid)
            {
                return View(contact);
            }
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            TempData["Success"] = "İletişim Bilgisi Kaydedildi";
            return RedirectToAction("ContactList");
        }
        
        public IActionResult UpdateContact(int id)
        {
            var value = _context.Contacts.Find(id);
            if (value == null) return NotFound();
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateContact(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }

            // 1) Veritabanındaki gerçek kaydı çek (CV alanları dolu haliyle gelir)
            var value = _context.Contacts.Find(contact.ContactId);
            if (value == null) return NotFound();

            // 2) Sadece formdaki alanları üzerine kopyala
            value.Email = contact.Email;
            value.Location = contact.Location;
            value.Linkedin = contact.Linkedin;
            value.Github = contact.Github;
            value.Description = contact.Description;
            value.SubDescription = contact.SubDescription;
            // CvFilePath ve CvDownloadCount'a dokunmuyoruz → eski değerleri korunur

            // 3) Kaydet
            _context.SaveChanges();
            TempData["Success"] = "Site ayarları güncellendi.";
            return RedirectToAction("ContactList");
        }

        [HttpPost]
        public IActionResult UploadCv(IFormFile? cvFile)
        {
            var contact = _context.Contacts.FirstOrDefault();
            if (contact == null) return RedirectToAction("ContactList");

            // Kontroller: dosya var mı, PDF mi, 5 MB'tan küçük mü?
            if (cvFile == null || cvFile.Length == 0)
            {
                TempData["Error"] = "Lütfen bir dosya seçiniz.";
                return RedirectToAction("ContactList");
            }
            if (Path.GetExtension(cvFile.FileName).ToLower() != ".pdf")
            {
                TempData["Error"] = "Sadece PDF dosyası yükleyebilirsiniz.";
                return RedirectToAction("ContactList");
            }
            if (cvFile.Length > 5 * 1024 * 1024)
            {
                TempData["Error"] = "CV dosyası en fazla 5 MB olabilir.";
                return RedirectToAction("ContactList");
            }

            // wwwroot/cv klasörüne benzersiz isimle kaydet
            var folder = Path.Combine(_env.WebRootPath, "cv");
            Directory.CreateDirectory(folder);
            var fileName = $"{Guid.NewGuid()}.pdf";

            using (var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create))
            {
                cvFile.CopyTo(stream);
            }

            // Eski CV'yi diskten sil
            if (!string.IsNullOrEmpty(contact.CvFilePath))
            {
                var oldPath = Path.Combine(_env.WebRootPath, contact.CvFilePath.TrimStart('/'));
                if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
            }

            contact.CvFilePath = $"/cv/{fileName}";
            _context.SaveChanges();

            TempData["Success"] = "CV başarıyla yüklendi.";
            return RedirectToAction("ContactList");
        }


    }
}
