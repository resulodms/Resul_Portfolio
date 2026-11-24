using System.ComponentModel.DataAnnotations;

namespace Resul_Portfolio.Entities
{
    public class Experience
    {
        public int ExperienceId { get; set; }

        [Required(ErrorMessage = "Pozisyon boş bırakılamaz.")]
        [StringLength(100, ErrorMessage = "Pozisyon en fazla 100 karakter olabilir.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Şirket adı boş bırakılamaz.")]
        [StringLength(100, ErrorMessage = "Şirket adı en fazla 100 karakter olabilir.")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Açıklama boş bırakılamaz.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Yan başlık boş bırakılamaz.")]
        public string SideTitle { get; set; }

        [Required(ErrorMessage = "Süre boş bırakılamaz.")]
        [RegularExpression(@"^\d{4}\s*[-–]\s*(\d{4}|Günümüz)$",
            ErrorMessage = "Süre '2022 – 2024' veya '2024 – Günümüz' formatında olmalıdır.")]
        public string Duration { get; set; }

        [Required(ErrorMessage = "Çalışma türü boş bırakılamaz.")]
        public string WorkType { get; set; }

        public string? Tags { get; set; }

        public bool IsActive { get; set; }

    }
}
