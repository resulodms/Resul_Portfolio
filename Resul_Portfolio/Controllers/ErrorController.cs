using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Resul_Portfolio.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{code:int}")]
        public IActionResult Index(int code)
        {
            // Kullanıcının asıl gitmeye çalıştığı adres
            var feature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            var originalPath = feature?.OriginalPath ?? "";

            ViewBag.Code = code;
            ViewBag.Path = originalPath;
            ViewBag.IsAdmin = originalPath.StartsWith("/Admin", StringComparison.OrdinalIgnoreCase);

            ViewBag.Title = code switch
            {
                404 => "Sayfa bulunamadı",
                403 => "Bu sayfaya erişim izniniz yok",
                _ => "Bir şeyler ters gitti"
            };
            ViewBag.Description = code switch
            {
                404 => "Aradığınız sayfa taşınmış, silinmiş ya da hiç var olmamış olabilir.",
                403 => "Bu içeriği görüntülemek için yetkiniz bulunmuyor.",
                _ => "Beklenmeyen bir hata oluştu. Lütfen biraz sonra tekrar deneyin."
            };

            Response.StatusCode = code; // tarayıcıya doğru durum kodu gitsin
            return View();
        }
    }
}