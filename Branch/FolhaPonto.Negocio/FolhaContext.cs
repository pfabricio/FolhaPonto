using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using FolhaPonto.Negocio.Model;
using FolhaPonto.Negocio.Model.Maps;

namespace FolhaPonto.Negocio
{
    public class FolhaContext: DbContext
    {
        /// <summary>
        /// Construtor da classe do contexto e passa como paramentro o nome da string de conexão
        /// </summary>
        public FolhaContext() : base("pontoDb") { }

        public DbSet<FuncionarioMo> Funcionarios { get; set; }
        public DbSet<CargoMo> Cargos { get; set; }
        public DbSet<JornadaMo> Jornadas { get; set; }
        public DbSet<MenuMo> Menus { get; set; }
        public DbSet<PerfilMo> Perfis { get; set; }
        public DbSet<UnidadeMo> Unidades { get; set; }
        public DbSet<JustificativaMo> Justificativas { get; set; }
        public DbSet<FrequenciaMo> Frequencias { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.Add(new CargoMap());
            modelBuilder.Configurations.Add(new FuncionarioMap());
            modelBuilder.Configurations.Add(new JornadaMap());
            modelBuilder.Configurations.Add(new MenuMap());
            modelBuilder.Configurations.Add(new PerfilMap());
            modelBuilder.Configurations.Add(new UnidadeMap());
            modelBuilder.Configurations.Add(new JustificativaMap());
            modelBuilder.Configurations.Add(new FrequenciaMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
