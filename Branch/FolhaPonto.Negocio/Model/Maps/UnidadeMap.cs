using System.Data.Entity.ModelConfiguration;

namespace FolhaPonto.Negocio.Model.Maps
{
    public class UnidadeMap: EntityTypeConfiguration<UnidadeMo>
    {
        public UnidadeMap()
        {
            ToTable("Unidades");
            Property(x => x.NomeUnidade).HasColumnType("varchar").HasMaxLength(80);
        }
    }
}
