namespace BelajarCRUDWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtblmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TB_M_Item",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Int(nullable: false),
                        Stock = c.Int(nullable: false),
                        Supplier_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TB_M_Supplier", t => t.Supplier_Id)
                .Index(t => t.Supplier_Id);
            
            CreateTable(
                "dbo.TB_M_Supplier",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Ket_Role = c.String(),
                        Role_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TB_M_Role", t => t.Role_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.TB_M_Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TB_M_Item", "Supplier_Id", "dbo.TB_M_Supplier");
            DropForeignKey("dbo.TB_M_Supplier", "Role_Id", "dbo.TB_M_Role");
            DropIndex("dbo.TB_M_Supplier", new[] { "Role_Id" });
            DropIndex("dbo.TB_M_Item", new[] { "Supplier_Id" });
            DropTable("dbo.TB_M_Role");
            DropTable("dbo.TB_M_Supplier");
            DropTable("dbo.TB_M_Item");
        }
    }
}
