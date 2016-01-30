using System.Data.Entity.ModelConfiguration;

namespace FolhaPonto.Negocio.Model.Maps
{
    public class JustificativaMap: EntityTypeConfiguration<JustificativaMo>
    {
        public JustificativaMap()
        {
            ToTable("Justificativa");
            Property(x => x.Inicio).HasColumnType("time").HasPrecision(7);
            Property(x => x.Fim).HasColumnType("time").HasPrecision(7);

            HasRequired(x=>x.Funcionario)
                .WithMany(x=>x.Justificativas)
                .HasForeignKey(x=>x.FuncionarioId)
                .WillCascadeOnDelete(false);
        }
    }
}
