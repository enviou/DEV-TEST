namespace MuralDeRecados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Murals",
                c => new
                    {
                        MuralId = c.Int(nullable: false, identity: true),
                        Texto = c.String(maxLength: 200),
                        DataCriacao = c.DateTime(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MuralId)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Senha = c.String(),
                        Telefone = c.String(),
                        LoginRedeSocial = c.String(),
                    })
                .PrimaryKey(t => t.UsuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Murals", "UsuarioId", "dbo.Usuarios");
            DropIndex("dbo.Murals", new[] { "UsuarioId" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Murals");
        }
    }
}
