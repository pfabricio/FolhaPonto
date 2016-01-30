using System.ComponentModel.DataAnnotations;

namespace FolhaPonto.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Login")]
        [Required(ErrorMessage = "Login é requerido")]
        public string Login { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Senha é requerida")]
        public string Senha { get; set; }
    }
}