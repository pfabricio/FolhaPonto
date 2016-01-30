using System.Data.Entity.ModelConfiguration;

namespace FolhaPonto.Negocio.Model.Maps
{
    /// <summary>
    /// Classe responsável mapear a tabela Cargo no banco de dados
    /// </summary>
    public class CargoMap: EntityTypeConfiguration<CargoMo>
    {
        public CargoMap()
        {
            ToTable("Cargos");
            Property(x => x.NomeCargo).HasColumnType("varchar").HasMaxLength(80);
        }
    }
}
