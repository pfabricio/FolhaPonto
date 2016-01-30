using System.Data.Entity.ModelConfiguration;

namespace FolhaPonto.Negocio.Model.Maps
{
    public class PerfilMap: EntityTypeConfiguration<PerfilMo>
    {
        public PerfilMap()
        {
            ToTable("Perfil");
            Property(x => x.NomePergil).HasColumnType("varchar").HasMaxLength(80);
        }
    }
}
