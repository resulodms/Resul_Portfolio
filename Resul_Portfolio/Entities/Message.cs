using System.ComponentModel.DataAnnotations;

namespace Resul_Portfolio.Entities
{
    public class Message
    {
        public int MessageId { get; set; }

        [Required(ErrorMessage = "Ad soyad boş bırakılamaz.")]
        [StringLength(100, ErrorMessage = "Ad soyad en fazla 100 karakter olabilir.")]
        public string NameSurname { get; set; }

        [Required(ErrorMessage = "E-posta boş bırakılamaz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Konu boş bırakılamaz.")]
        [StringLength(150, ErrorMessage = "Konu en fazla 150 karakter olabilir.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Mesaj boş bırakılamaz.")]
        [StringLength(2000, MinimumLength = 10, ErrorMessage = "Mesaj 10-2000 karakter arasında olmalıdır.")]
        public string MessageDetail { get; set; }

        public DateTime SendDate { get; set; }
        public bool IsRead { get; set; }
    }
}
