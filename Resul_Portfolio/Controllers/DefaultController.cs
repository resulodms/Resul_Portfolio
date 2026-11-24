using Microsoft.AspNetCore.Mvc;
using Resul_Portfolio.Context;
using Resul_Portfolio.Entities;

namespace Resul_Portfolio.Controllers
{
    public class DefaultController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly IWebHostEnvironment _env;

        public DefaultController(PortfolioContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendMessage(Message message)
        {
            message.SendDate = DateTime.Now;
            message.IsRead = false;

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(x=>x.Value.Errors.Any()).ToDictionary(X=>X.Key, x=>x.Value.Errors.First().ErrorMessage);
                return BadRequest(new { success = false, errors });
            }
            
            _context.Messages.Add(message);
            _context.SaveChanges();
            return Ok(new { success = true, message = "Mesajınız Basarıyla iletildi." });

        }

        public IActionResult DownloadCv()
        {
            var contact = _context.Contacts.FirstOrDefault();

            // CV yüklenmemişse ana sayfadaki iletişim bölümüne yönlendir
            if (contact == null || string.IsNullOrEmpty(contact.CvFilePath))
                return Redirect("/#contact");

            var path = Path.Combine(_env.WebRootPath, contact.CvFilePath.TrimStart('/'));
            if (!System.IO.File.Exists(path))
                return Redirect("/#contact");

            // İndirme sayısını artır (Dashboard'da gösterilecek)
            contact.CvDownloadCount++;
            _context.SaveChanges();

            // Tarayıcıya dosyayı "Resul_Odemis_CV.pdf" adıyla indir
            return PhysicalFile(path, "application/pdf", "Resul_Odemis_CV.pdf");
        }













    }
}
