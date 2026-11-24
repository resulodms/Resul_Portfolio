using System.ComponentModel.DataAnnotations;

namespace Resul_Portfolio.Entities
{
    public class Skill
    {
        public int SkillId { get; set; }

        [Required(ErrorMessage = "Yetenek adı boş bırakılamaz.")]
        [StringLength(50, ErrorMessage = "Yetenek adı en fazla 50 karakter olabilir.")]
        public string Name { get; set; }

        [Range(0, 100, ErrorMessage = "Seviye 0 ile 100 arasında olmalıdır.")]
        public int Percentage { get; set; }

        [Required(ErrorMessage = "Grup boş bırakılamaz.")]
        public string GroupName { get; set; }

        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
