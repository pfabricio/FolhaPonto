using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FolhaPonto.Negocio.Model
{
    public class FuncionarioMo
    {
        [Key]
        public int Id { get; set; }
        public int CargoId { get; set; }
        public int UnidadeId { get; set; }
        public int JornadaId { get; set; }
        public int PerfilId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

        [ForeignKey("CargoId")]
        public virtual CargoMo Cargo { get; set; }

        [ForeignKey("UnidadeId")]
        public virtual UnidadeMo Unidade { get; set; }

        [ForeignKey("PerfilId")]
        public virtual PerfilMo Perfil { get; set; }

        [ForeignKey("JornadaId")]
        public virtual JornadaMo Jornada { get; set; }
        public virtual  ICollection<JustificativaMo> Justificativas  { get; set; }
        public virtual  ICollection<FrequenciaMo> Frequencias { get; set; }
    }
}
