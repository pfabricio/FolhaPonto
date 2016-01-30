using System.ComponentModel.DataAnnotations;

namespace FolhaPonto.Models
{
    public class RelatorioMetasViewModel
    {
        [Display(Name = "Funcionário")]
        [Required(ErrorMessage = "Funcionário é obrigatório", AllowEmptyStrings = false)]
        public string Funcionario { get; set; }

        public int Funcionarioid { get; set; }

        [Display(Name = "Data 1")]
        [Required(ErrorMessage = "Data 1 é obrigatório", AllowEmptyStrings = false)]
        public string Data1 { get; set; }

        [Display(Name = "Data 2")]
        [Required(ErrorMessage = "Data 2 é obrigatório", AllowEmptyStrings = false)]
        public string Data2 { get; set; }
    }
}