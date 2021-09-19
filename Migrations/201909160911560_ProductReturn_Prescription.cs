namespace WebSolutionForModelPharmacies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductReturn_Prescription : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Prescriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PrescriptionImg = c.String(),
                        Status = c.String(),
                        Cutomer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Cutomer_Id)
                .Index(t => t.Cutomer_Id);
            
            CreateTable(
                "dbo.ProductReturns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Customer_Id = c.Int(),
                        OrderItem_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.OrderItems", t => t.OrderItem_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.OrderItem_Id);
            
            AddColumn("dbo.Products", "ExpiryDate", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductReturns", "OrderItem_Id", "dbo.OrderItems");
            DropForeignKey("dbo.ProductReturns", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Prescriptions", "Cutomer_Id", "dbo.Customers");
            DropIndex("dbo.ProductReturns", new[] { "OrderItem_Id" });
            DropIndex("dbo.ProductReturns", new[] { "Customer_Id" });
            DropIndex("dbo.Prescriptions", new[] { "Cutomer_Id" });
            DropColumn("dbo.Products", "ExpiryDate");
            DropTable("dbo.ProductReturns");
            DropTable("dbo.Prescriptions");
        }
    }
}
