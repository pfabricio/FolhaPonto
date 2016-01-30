using System;
using System.ComponentModel.DataAnnotations;

namespace FolhaPonto.Models
{
    public class JornadaViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Cargo")]
        [Required(ErrorMessage = "Cargo é obrigatório", AllowEmptyStrings = false)]
        public int CargoId { get; set; }

        [Display(Name = "Hora da Entrada")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Hora da Entrada é obrigatório", AllowEmptyStrings = false)]
        public TimeSpan HoraEntrada { get; set; }

        [Display(Name = "Hora da Saída")]
        [Required(ErrorMessage = "Hora da Saída é obrigatório", AllowEmptyStrings = false)]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan HoraSaida { get; set; }

        [Display(Name = "Intervalo")]
        [Required(ErrorMessage = "Intervalo é obrigatório", AllowEmptyStrings = false)]
        public int TempoIntervalo { get; set; }

        [Display(Name = "Meta")]
        [Required(ErrorMessage = "Meta é obrigatório", AllowEmptyStrings = false)]
        public int TempoMeta { get; set; }
    }
}