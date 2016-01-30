
using System.ComponentModel.DataAnnotations;

namespace FolhaPonto.Models
{
    public class FrequenciaListViewModel
    {
        public int Id { get; set; }
        public string FuncionarioNome { get; set; }
        public string Data { get; set; }
        public string Entrada { get; set; }
        [Display(Name = "Entrada do intervalo")]
        public string EntradaIntervalo { get; set; }
        [Display(Name = "Volta do intervalo")]
        public string SaidaIntervalo { get; set; }
        [Display(Name = "Saída")]
        public string Saida { get; set; }

        [Display(Name = "Hora trabalhada")]
        public decimal HoraTrabalhada { get; set; }
    }
}