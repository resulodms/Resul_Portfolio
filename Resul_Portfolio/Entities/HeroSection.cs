using System.ComponentModel.DataAnnotations;

namespace Resul_Portfolio.Entities
{
    public class HeroSection
    {
        public int HeroSectionId { get; set; }

        [Required(ErrorMessage = "Karşılama yazısı boş bırakılamaz.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Ad boş bırakılamaz.")]
        public string SubTitle { get; set; }

        [Required(ErrorMessage = "Tanıtım metni boş bırakılamaz.")]
        [StringLength(500, ErrorMessage = "Tanıtım metni en fazla 500 karakter olabilir.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Profil görseli boş bırakılamaz.")]
        public string ImageUrl { get; set; }

    }
}
