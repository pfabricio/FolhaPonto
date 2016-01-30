using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FolhaPonto.Negocio.Model
{
    public class CargoMo
    {
        [Key]
        public int Id { get; set; }
        public string NomeCargo { get; set; }
        public virtual ICollection<FuncionarioMo> Funcionarios  { get; set; }
        public virtual ICollection<JornadaMo> Jornadas { get; set; }
    }
}
