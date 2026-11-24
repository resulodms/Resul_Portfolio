using System.ComponentModel.DataAnnotations;

namespace Resul_Portfolio.Entities
{
    public class Education
    {
        public int EducationId { get; set; }

        [Required(ErrorMessage = "Okul adı boş bırakılamaz.")]
        [StringLength(150, ErrorMessage = "Okul adı en fazla 150 karakter olabilir.")]
        public string Title { get; set; }

        public string? SubTitle { get; set; }

        [Required(ErrorMessage = "Açıklama boş bırakılamaz.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Süre boş bırakılamaz.")]
        [RegularExpression(@"^\d{4}\s*[-–]\s*(\d{4}|Günümüz)$",
            ErrorMessage = "Süre '2016 – 2020' veya '2024 – Günümüz' formatında olmalıdır.")]
        public string Duration { get; set; }

        public string? Location { get; set; }
        public string? Tags { get; set; }
        public bool IsActive { get; set; }
    }
}
