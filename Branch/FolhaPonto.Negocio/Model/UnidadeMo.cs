using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FolhaPonto.Negocio.Model
{
    public class UnidadeMo
    {
        [Key]
        public int Id { get; set; }
        public string NomeUnidade { get; set; }
        public virtual ICollection<FuncionarioMo> Funcionarios { get; set; }
    }
}
