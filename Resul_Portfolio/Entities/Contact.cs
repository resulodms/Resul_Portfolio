using System.ComponentModel.DataAnnotations;

namespace Resul_Portfolio.Entities
{
    public class Contact
    {
        public int ContactId { get; set; }

        [Required(ErrorMessage = "Açıklama boş bırakılamaz.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "E-posta boş bırakılamaz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "LinkedIn linki boş bırakılamaz.")]
        public string Linkedin { get; set; }

        [Required(ErrorMessage = "Konum boş bırakılamaz.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "GitHub linki boş bırakılamaz.")]
        public string Github { get; set; }

        [Required(ErrorMessage = "Alt açıklama boş bırakılamaz.")]
        public string SubDescription { get; set; }

        public string? CvFilePath { get; set; }
        public int CvDownloadCount { get; set; }

    }
}
