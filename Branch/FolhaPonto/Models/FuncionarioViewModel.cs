using System.ComponentModel.DataAnnotations;

namespace FolhaPonto.Models
{
    public class FuncionarioViewModel
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Cargo")]
        [Required(ErrorMessage = "Cargo é obrigatório", AllowEmptyStrings = false)]
        public int CargoId { get; set; }

        [Display(Name = "Unidade")]
        [Required(ErrorMessage = "Unidade é obrigatório", AllowEmptyStrings = false)]
        public int UnidadeId { get; set; }

        [Display(Name = "Jornada")]
        [Required(ErrorMessage = "Jornada é obrigatório", AllowEmptyStrings = false)]
        public int JornadaId { get; set; }

        [Display(Name = "Perfil")]
        [Required(ErrorMessage = "Perfil é obrigatório", AllowEmptyStrings = false)]
        public int PerfilId { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório", AllowEmptyStrings = false)]
        public string Nome { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "E-mail é obrigatório", AllowEmptyStrings = false)]
        public string Email { get; set; }

        [Display(Name = "Endereço")]
        [Required(ErrorMessage = "Endereço é obrigatório", AllowEmptyStrings = false)]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório", AllowEmptyStrings = false)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Login é obrigatório", AllowEmptyStrings = false)]
        public string Login { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name = "Confirmar Senha")]
        [Required(ErrorMessage = "Confirmar Senha é obrigatório", AllowEmptyStrings = false)]
        [Compare("Senha", ErrorMessage = "As senhas digitadas não conferem.")]
        [DataType(DataType.Password)]
        public string ConfirmaSenha { get; set; }
    }
}