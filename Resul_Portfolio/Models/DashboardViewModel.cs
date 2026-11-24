using Resul_Portfolio.Entities;

namespace Resul_Portfolio.Models
{
    public class DashboardViewModel
    {
        public int ProjectCount { get; set; }
        public int ActiveProjectCount { get; set; }
        public int CategoryCount { get; set; }
        public int SkillCount { get; set; }
        public int ExperienceCount { get; set; }
        public int EducationCount { get; set; }
        public int MessageCount { get; set; }
        public int UnreadMessageCount { get; set; }
        public int CvDownloadCount { get; set; }

        // Listeler
        public List<Message> LastMessages { get; set; } = new();
        public List<Project> LastProjects { get; set; } = new();

        // Kategori bazında proje sayısı (GroupBy sonucu)
        public List<CategoryProjectCount> CategoryStats { get; set; } = new();
    }
    public class CategoryProjectCount
    {
        public string CategoryName { get; set; } = "";
        public int ProjectCount { get; set; }
    }
}
