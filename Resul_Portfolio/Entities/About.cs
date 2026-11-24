using System.ComponentModel.DataAnnotations;

namespace Resul_Portfolio.Entities
{
    public class About
    {
        public int AboutId { get; set; }

        [Required(ErrorMessage = "Ad soyad boş bırakılamaz.")]
        [StringLength(100, ErrorMessage = "Ad soyad en fazla 100 karakter olabilir.")]
        public string NameSurname { get; set; }

        [Required(ErrorMessage = "Meslek boş bırakılamaz.")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Konum boş bırakılamaz.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Uzmanlık boş bırakılamaz.")]
        public string Expert { get; set; }

        [Required(ErrorMessage = "E-posta boş bırakılamaz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Açıklama boş bırakılamaz.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Çalışma durumu boş bırakılamaz.")]
        public string OpenToWork { get; set; }
    }
}
