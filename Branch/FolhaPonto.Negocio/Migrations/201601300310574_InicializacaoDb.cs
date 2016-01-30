namespace FolhaPonto.Negocio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InicializacaoDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cargos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomeCargo = c.String(maxLength: 80, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Funcionarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CargoId = c.Int(nullable: false),
                        UnidadeId = c.Int(nullable: false),
                        JornadaId = c.Int(nullable: false),
                        PerfilId = c.Int(nullable: false),
                        Nome = c.String(maxLength: 180, unicode: false),
                        Email = c.String(maxLength: 120, unicode: false),
                        Endereco = c.String(maxLength: 180, unicode: false),
                        Telefone = c.String(maxLength: 15, unicode: false),
                        Login = c.String(maxLength: 80, unicode: false),
                        Senha = c.String(maxLength: 200, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cargos", t => t.CargoId)
                .ForeignKey("dbo.Jornadas", t => t.JornadaId)
                .ForeignKey("dbo.Perfil", t => t.PerfilId)
                .ForeignKey("dbo.Unidades", t => t.UnidadeId)
                .Index(t => t.CargoId)
                .Index(t => t.UnidadeId)
                .Index(t => t.JornadaId)
                .Index(t => t.PerfilId);
            
            CreateTable(
                "dbo.Frequencia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FuncionarioId = c.Int(nullable: false),
                        Data = c.DateTime(nullable: false, storeType: "date"),
                        Entrada = c.Time(nullable: false, precision: 7),
                        SaidaIntervalo = c.Time(nullable: false, precision: 7),
                        VoltaIntervalo = c.Time(nullable: false, precision: 7),
                        Saida = c.Time(nullable: false, precision: 7),
                        HoraTrabalhada = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsEntrada = c.Boolean(nullable: false),
                        IsSaidaIntervalo = c.Boolean(nullable: false),
                        IsVoltaIntervalo = c.Boolean(nullable: false),
                        IsSaida = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Funcionarios", t => t.FuncionarioId)
                .Index(t => t.FuncionarioId);
            
            CreateTable(
                "dbo.Jornadas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CargoId = c.Int(nullable: false),
                        HoraEntrada = c.Time(nullable: false, precision: 7),
                        HoraSaida = c.Time(nullable: false, precision: 7),
                        TempoIntervalo = c.Int(nullable: false),
                        TempoMeta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cargos", t => t.CargoId)
                .Index(t => t.CargoId);
            
            CreateTable(
                "dbo.Justificativa",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FuncionarioId = c.Int(nullable: false),
                        Data = c.DateTime(nullable: false),
                        Inicio = c.Time(nullable: false, precision: 7),
                        Fim = c.Time(nullable: false, precision: 7),
                        Texto = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Funcionarios", t => t.FuncionarioId)
                .Index(t => t.FuncionarioId);
            
            CreateTable(
                "dbo.Perfil",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomePergil = c.String(maxLength: 80, unicode: false),
                        Peso = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Unidades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomeUnidade = c.String(maxLength: 80, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Action = c.String(maxLength: 80, unicode: false),
                        Controller = c.String(maxLength: 80, unicode: false),
                        Imagem = c.String(maxLength: 120, unicode: false),
                        Peso = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Funcionarios", "UnidadeId", "dbo.Unidades");
            DropForeignKey("dbo.Funcionarios", "PerfilId", "dbo.Perfil");
            DropForeignKey("dbo.Justificativa", "FuncionarioId", "dbo.Funcionarios");
            DropForeignKey("dbo.Funcionarios", "JornadaId", "dbo.Jornadas");
            DropForeignKey("dbo.Jornadas", "CargoId", "dbo.Cargos");
            DropForeignKey("dbo.Frequencia", "FuncionarioId", "dbo.Funcionarios");
            DropForeignKey("dbo.Funcionarios", "CargoId", "dbo.Cargos");
            DropIndex("dbo.Justificativa", new[] { "FuncionarioId" });
            DropIndex("dbo.Jornadas", new[] { "CargoId" });
            DropIndex("dbo.Frequencia", new[] { "FuncionarioId" });
            DropIndex("dbo.Funcionarios", new[] { "PerfilId" });
            DropIndex("dbo.Funcionarios", new[] { "JornadaId" });
            DropIndex("dbo.Funcionarios", new[] { "UnidadeId" });
            DropIndex("dbo.Funcionarios", new[] { "CargoId" });
            DropTable("dbo.Menus");
            DropTable("dbo.Unidades");
            DropTable("dbo.Perfil");
            DropTable("dbo.Justificativa");
            DropTable("dbo.Jornadas");
            DropTable("dbo.Frequencia");
            DropTable("dbo.Funcionarios");
            DropTable("dbo.Cargos");
        }
    }
}
