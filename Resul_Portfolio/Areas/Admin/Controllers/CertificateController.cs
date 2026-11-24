using Microsoft.AspNetCore.Mvc;
using Resul_Portfolio.Context;
using Resul_Portfolio.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Resul_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CertificateController : Controller
    {
        private readonly PortfolioContext _context;

        public CertificateController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult CertificateList()
        {
            var values = _context.Certificates.OrderByDescending(x => x.IssueDate).ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateCertificate()
        {
            return View(new Certificate { IssueDate=DateTime.Today, IsActive=true });
        }
        [HttpPost]
        public IActionResult CreateCertificate(Certificate certificate)
        {
            if (!ModelState.IsValid)
            {
                return View(certificate);
            }
            _context.Certificates.Add(certificate);
            _context.SaveChanges();
            TempData["Success"] = "Sertifika Bilgisi Kaydedildi";
            return RedirectToAction("CertificateList");
        }
        public IActionResult DeleteCertificate(int id)
        {

            var value = _context.Certificates.Find(id);
            if (value == null) return NotFound();
            _context.Certificates.Remove(value);
            _context.SaveChanges();
            TempData["Success"] = "Sertifika Bilgisi Silindi";
            return RedirectToAction("CertificateList");
        }
        public IActionResult UpdateCertificate(int id)
        {
            var value = _context.Certificates.Find(id);
            if (value == null) return NotFound();
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateCertificate(Certificate certificate)
        {
            if (!ModelState.IsValid)
            {
                return View(certificate);
            }
            _context.Certificates.Update(certificate);
            _context.SaveChanges();
            TempData["Success"] = "Sertifika Bilgisi Güncellendi";
            return RedirectToAction("CertificateList");
        }
    }
}
