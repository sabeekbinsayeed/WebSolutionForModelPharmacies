namespace WebSolutionForModelPharmacies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderReturn : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReturnProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Street = c.String(),
                        City = c.String(),
                        Zip = c.Int(nullable: false),
                        AskingReturnPrice = c.String(),
                        ExpiryDate = c.String(),
                        Status = c.String(),
                        Customer_Id = c.Int(),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReturnProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ReturnProducts", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.ReturnProducts", new[] { "Product_Id" });
            DropIndex("dbo.ReturnProducts", new[] { "Customer_Id" });
            DropTable("dbo.ReturnProducts");
        }
    }
}
