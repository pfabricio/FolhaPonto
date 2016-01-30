using System.ComponentModel.DataAnnotations;

namespace FolhaPonto.Models
{
    public class JornadaListViewModel
    {
        public int Id { get; set; }

        public string Cargo { get; set; }

        [Display(Name = "Entrada")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = false)]
        public string HoraEntrada { get; set; }

        [Display(Name = "Saída")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = false)]
        public string HoraSaida { get; set; }

        [Display(Name = "Intervalo")]
        public int TempoIntervalo { get; set; }

        [Display(Name = "Meta")]
        public int TempoMeta { get; set; }
    }
}