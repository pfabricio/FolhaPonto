using System;

namespace FolhaPonto.Models
{
    public class FrequenciaViewModel
    {
        public int Id { get; set; }
        public int FuncionarioId { get; set; }
        public int TipoSaida { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Hora { get; set; }
    }
}