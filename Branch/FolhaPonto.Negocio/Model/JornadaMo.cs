using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FolhaPonto.Negocio.Model
{
    public class JornadaMo
    {
        public int Id { get; set; }
        public int CargoId { get; set; }
        public TimeSpan HoraEntrada { get; set; }
        public TimeSpan HoraSaida { get; set; }
        public int TempoIntervalo { get; set; }
        public int TempoMeta { get; set; }

        [NotMapped]
        public string Descricao
        {
            get { return string.Format("{0} às {1}", HoraEntrada.ToString(@"hh\:mm"), HoraSaida.ToString(@"hh\:mm")); }
        }

        [ForeignKey("CargoId")]
        public CargoMo Cargo { get; set; }

        public virtual  ICollection<FuncionarioMo> Funcionarios { get; set; }
    }
}
