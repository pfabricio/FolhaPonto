using System.Data.Entity.ModelConfiguration;

namespace FolhaPonto.Negocio.Model.Maps
{
    public class FuncionarioMap: EntityTypeConfiguration<FuncionarioMo>
    {
        public FuncionarioMap()
        {
            //Nome da Tabela
            ToTable("Funcionarios");

            //Configurações dos campos da Tabela --------------------------------------
            Property(x => x.Nome).HasColumnType("varchar").HasMaxLength(180);
            Property(x => x.Email).HasColumnType("varchar").HasMaxLength(120);
            Property(x => x.Endereco).HasColumnType("varchar").HasMaxLength(180);
            Property(x => x.Telefone).HasColumnType("varchar").HasMaxLength(15);
            Property(x => x.Login).HasColumnType("varchar").HasMaxLength(80);
            Property(x => x.Senha).HasColumnType("varchar").HasMaxLength(200);
            // -------------------------------------------------------------------------

            // Criação das ForeignKeys one-to-many ----------------
            HasRequired(x => x.Cargo)
                .WithMany(x => x.Funcionarios)
                .HasForeignKey(x => x.CargoId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Unidade)
                .WithMany(x => x.Funcionarios)
                .HasForeignKey(x => x.UnidadeId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Perfil)
                .WithMany(x => x.Funcionarios)
                .HasForeignKey(x => x.PerfilId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Jornada)
                .WithMany(x => x.Funcionarios)
                .HasForeignKey(x => x.JornadaId)
                .WillCascadeOnDelete(false);
            // ----------------------------------------------
        }
    }
}
