using System.ComponentModel.DataAnnotations;

namespace Resul_Portfolio.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı Boş Bırakılamaz")]
        public string UserName { get; set; } = "";

        [Required(ErrorMessage = "Şifre Boş Bırakılamaz.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }




    }
}
