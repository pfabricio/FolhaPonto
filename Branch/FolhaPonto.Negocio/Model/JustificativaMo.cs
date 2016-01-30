using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FolhaPonto.Negocio.Model
{
    public class JustificativaMo
    {
        [Key]
        public int Id { get; set; }
        public int FuncionarioId { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Inicio { get; set; }
        public TimeSpan Fim { get; set; }
        public string Texto { get; set; }

        [ForeignKey("FuncionarioId")]
        public virtual FuncionarioMo Funcionario { get; set; }
    }
}
