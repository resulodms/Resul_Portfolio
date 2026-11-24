using System.ComponentModel.DataAnnotations;

namespace Resul_Portfolio.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Proje adı boş bırakılamaz.")]
        [StringLength(100, ErrorMessage = "Proje adı en fazla 100 karakter olabilir.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Açıklama boş bırakılamaz.")]
        public string Description { get; set; }

        public string? GithubUrl { get; set; }
        public string? LiveDemoUrl { get; set; }

        [Required(ErrorMessage = "Kapak görseli boş bırakılamaz.")]
        public string CoverImageUrl { get; set; }

        public string? Tags { get; set; }
        public bool IsActive { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Lütfen bir kategori seçiniz.")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
