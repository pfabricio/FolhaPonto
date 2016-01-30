using System.Data.Entity.ModelConfiguration;

namespace FolhaPonto.Negocio.Model.Maps
{
    public class FrequenciaMap: EntityTypeConfiguration<FrequenciaMo>
    {
        public FrequenciaMap()
        {
            ToTable("Frequencia");
            Property(x => x.Data).HasColumnType("date");
            Property(x => x.Entrada).HasColumnType("time").HasPrecision(7);
            Property(x => x.EntradaIntervalo).HasColumnType("time").HasPrecision(7);
            Property(x => x.SaidaIntervalo).HasColumnType("time").HasPrecision(7);
            Property(x => x.Saida).HasColumnType("time").HasPrecision(7);
            Property(x => x.HoraTrabalhada).HasColumnType("decimal").HasPrecision(18,2);

            HasRequired(x => x.Funcionario)
                .WithMany(x => x.Frequencias)
                .HasForeignKey(x => x.FuncionarioId)
                .WillCascadeOnDelete(false);
        }
    }
}