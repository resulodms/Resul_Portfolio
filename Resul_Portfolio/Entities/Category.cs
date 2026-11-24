using System.ComponentModel.DataAnnotations;

namespace Resul_Portfolio.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Kategori adı boş bırakılamaz.")]
        [StringLength(50, ErrorMessage = "Kategori adı en fazla 50 karakter olabilir.")]
        public string CategoryName { get; set; }

        public List<Project> Projects { get; set; } = new List<Project>();
    }
}
