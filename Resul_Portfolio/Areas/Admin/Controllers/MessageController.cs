using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resul_Portfolio.Context;
using Resul_Portfolio.Entities;

namespace Resul_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class MessageController : Controller
    {
        private readonly PortfolioContext _context;

        public MessageController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult MessageList(string? search, string sort = "desc")
        {
            var query = _context.Messages
               .Where(x => search == null || x.NameSurname.Contains(search) || x.Subject.Contains(search));

            query = sort == "asc"
                ? query.OrderBy(x => x.SendDate)
                : query.OrderByDescending(x => x.SendDate);

            var values = query.ToList();
            ViewBag.Search = search;
            ViewBag.Sort = sort;
            ViewBag.UnreadCount = _context.Messages.Count(x => !x.IsRead);

            return View(values);
        }
        public IActionResult MarkAsRead(int id)
        {
            var value = _context.Messages.Find(id);
            if (value == null) return NotFound();

            value.IsRead = true;

            _context.SaveChanges();
            return RedirectToAction("MessageList");

        }

       
        public IActionResult DeleteMessage(int id)
        {
            var value = _context.Messages.Find(id);
            if (value == null) return NotFound();
            _context.Messages.Remove(value);
            _context.SaveChanges();
            TempData["Success"] = "Mesaj Silindi";
            return RedirectToAction("MessageList");
        }

       
    }

}
