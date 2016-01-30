using System;
using System.ComponentModel.DataAnnotations;

namespace FolhaPonto.Models
{
    public class JustificativaViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Funcionário")]
        [Required(ErrorMessage = "Funcionário é obrigatório.", AllowEmptyStrings = false)]
        public string FuncionarioNome { get; set; }

        public int FuncionarioId { get; set; }

        [Display(Name = "Data")]
        [Required(ErrorMessage = "Funcionário é obrigatório.", AllowEmptyStrings = false)]
        public DateTime Data { get; set; }

        [Display(Name = "Início")]
        [Required(ErrorMessage = "Hora 1 é obrigatório.", AllowEmptyStrings = false)]
        public TimeSpan Inicio { get; set; }

        [Display(Name = "Fim")]
        [Required(ErrorMessage = "Hora 2 é obrigatório.", AllowEmptyStrings = false)]
        public TimeSpan Fim { get; set; }

        [Display(Name = "Justificativa")]
        [Required(ErrorMessage = "Justificativa é obrigatório.", AllowEmptyStrings = false)]
        public string Texto { get; set; }
    }
}