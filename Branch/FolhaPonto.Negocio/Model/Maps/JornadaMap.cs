using System.Data.Entity.ModelConfiguration;

namespace FolhaPonto.Negocio.Model.Maps
{
    public class JornadaMap: EntityTypeConfiguration<JornadaMo>
    {
        public JornadaMap()
        {
            ToTable("Jornadas");
            Property(x => x.HoraEntrada).HasColumnType("time").HasPrecision(7);
            Property(x => x.HoraSaida).HasColumnType("time").HasPrecision(7);

            HasRequired(x => x.Cargo)
                .WithMany(x => x.Jornadas)
                .HasForeignKey(x => x.CargoId)
                .WillCascadeOnDelete(false);
        }
    }
}
