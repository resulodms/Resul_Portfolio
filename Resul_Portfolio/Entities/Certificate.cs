using System.ComponentModel.DataAnnotations;

namespace Resul_Portfolio.Entities
{
    public class Certificate
    {
        public int CertificateId { get; set; }

        [Required(ErrorMessage = "Sertifika adı boş bırakılamaz.")]
        [StringLength(150, ErrorMessage = "Sertifika adı en fazla 150 karakter olabilir.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Veren kurum boş bırakılamaz.")]
        public string Issuer { get; set; }

        public DateTime IssueDate { get; set; }
        public string? CertificateUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
