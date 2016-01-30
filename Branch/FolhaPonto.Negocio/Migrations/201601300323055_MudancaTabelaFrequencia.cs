namespace FolhaPonto.Negocio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MudancaTabelaFrequencia : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Frequencia", "EntradaIntervalo", c => c.Time(nullable: false, precision: 7));
            DropColumn("dbo.Frequencia", "VoltaIntervalo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Frequencia", "VoltaIntervalo", c => c.Time(nullable: false, precision: 7));
            DropColumn("dbo.Frequencia", "EntradaIntervalo");
        }
    }
}
