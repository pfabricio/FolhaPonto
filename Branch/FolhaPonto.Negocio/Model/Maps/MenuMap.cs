using System.Data.Entity.ModelConfiguration;

namespace FolhaPonto.Negocio.Model.Maps
{
    public class MenuMap: EntityTypeConfiguration<MenuMo>
    {
        public MenuMap()
        {
            ToTable("Menus");
            Property(x => x.Action).HasColumnType("varchar").HasMaxLength(80);
            Property(x => x.Controller).HasColumnType("varchar").HasMaxLength(80);
            Property(x => x.Imagem).HasColumnType("varchar").HasMaxLength(120);
        }
    }
}
