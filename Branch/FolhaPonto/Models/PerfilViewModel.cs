using System.ComponentModel.DataAnnotations;

namespace FolhaPonto.Models
{
    public class PerfilViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nome do Perfil")]
        [Required(ErrorMessage = "Nome do Perfil é obrigatório.", AllowEmptyStrings = false)]
        public string NomePerfil { get; set; }

        [Required(ErrorMessage = "Peso é obrigatório.", AllowEmptyStrings = false)]
        public int Peso { get; set; }
    }
}