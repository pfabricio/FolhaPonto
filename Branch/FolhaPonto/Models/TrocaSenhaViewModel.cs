using System.ComponentModel.DataAnnotations;

namespace FolhaPonto.Models
{
    public class TrocaSenhaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Login obrigatório", AllowEmptyStrings = false)]
        public string Login { get; set; }

        [Display(Name = "Senha Antiga")]
        [Required(ErrorMessage = "A senha antiga é obrigatória")]
        [DataType(DataType.Password)]
        public string SenhaAntiga { get; set; }

        [Display(Name = "Nova Antiga")]
        [Required(ErrorMessage = "A nova senha é obrigatória")]
        [DataType(DataType.Password)]
        public string SenhaNova { get; set; }

        [Display(Name = "Confirme a senha")]
        [Required(ErrorMessage = "A confirmação da senha é obrigatório")]
        [DataType(DataType.Password)]
        [Compare("SenhaNova", ErrorMessage = "As senhas não conferem")]
        public string ConfirmSenha { get; set; }
    }
}