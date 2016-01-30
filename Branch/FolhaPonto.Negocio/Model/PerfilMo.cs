using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FolhaPonto.Negocio.Model
{
    public class PerfilMo
    {
        [Key]
        public int Id { get; set; }
        public string NomePergil { get; set; }
        public int Peso { get; set; }
        public virtual ICollection<FuncionarioMo> Funcionarios  { get; set; }
    }
}
