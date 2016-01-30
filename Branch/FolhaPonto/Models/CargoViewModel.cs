using System.ComponentModel.DataAnnotations;

namespace FolhaPonto.Models
{
    public class CargoViewModel
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Nome do Cargo")]
        [Required(ErrorMessage = "Cargo é obrigatorio", AllowEmptyStrings = false)]
        public string NomeCargo { get; set; }
    }
}