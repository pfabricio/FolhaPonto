using System.ComponentModel.DataAnnotations;

namespace FolhaPonto.Models
{
    public class FuncionarioEditViewModel
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
    }
}