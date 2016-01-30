using System.ComponentModel.DataAnnotations;

namespace FolhaPonto.Models
{
    public class UnidadeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Unidade")]
        [Required(ErrorMessage = "Unidade é obrigatório", AllowEmptyStrings = false)]
        public string NomeUnidade { get; set; }
    }
}