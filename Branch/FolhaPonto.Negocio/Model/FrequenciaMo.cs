using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FolhaPonto.Negocio.Model
{
    public class FrequenciaMo
    {
        [Key]
        public int Id { get; set; }
        public int FuncionarioId { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Entrada { get; set; }
        public TimeSpan EntradaIntervalo { get; set; }
        public TimeSpan SaidaIntervalo { get; set; }
        public TimeSpan Saida { get; set; }
        public decimal HoraTrabalhada { get; set; }

        public bool IsEntrada { get; set; }
        public bool IsSaidaIntervalo { get; set; }
        public bool IsVoltaIntervalo { get; set; }
        public bool IsSaida { get; set; }

        [ForeignKey("FuncionarioId")]
        public virtual FuncionarioMo Funcionario { get; set; }
    }
}