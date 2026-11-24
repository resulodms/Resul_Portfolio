using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resul_Portfolio.Context;
using Resul_Portfolio.Models;
using Microsoft.AspNetCore.Authorization;

namespace Resul_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class DashboardController : Controller
    {
        
        private readonly PortfolioContext _context;

        public DashboardController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            var model = new DashboardViewModel
            {
                ProjectCount = _context.Projects.Count(),
                ActiveProjectCount = _context.Projects.Count(x=>x.IsActive),
                CategoryCount = _context.Categories.Count(),
                SkillCount = _context.Skills.Count(),
                ExperienceCount = _context.Experiences.Count(),
                EducationCount = _context.Educations.Count(),
                MessageCount = _context.Messages.Count(),
                UnreadMessageCount= _context.Messages.Count(x=>!x.IsRead),
                CvDownloadCount = _context.Contacts.Select(x=>x.CvDownloadCount).FirstOrDefault(),

                LastMessages = _context.Messages.OrderByDescending(x=>x.SendDate).Take(5).ToList(),

                LastProjects= _context.Projects.Include(x=>x.Category).OrderByDescending(y=>y.ProjectId).Take(3).ToList(),


                CategoryStats = _context.Projects.GroupBy(x=>x.Category!.CategoryName).Select(g=> new CategoryProjectCount
                {
                    CategoryName = g.Key,
                    ProjectCount = g.Count()
                }).OrderByDescending(y=>y.ProjectCount).ToList()




            };

            return View(model);
        }
    }
}



