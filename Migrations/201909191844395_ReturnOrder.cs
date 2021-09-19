namespace WebSolutionForModelPharmacies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReturnOrder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductReturnOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Street = c.String(),
                        Zip = c.Int(nullable: false),
                        City = c.String(),
                        TotalAskingPrice = c.Double(nullable: false),
                        TotalSalePrice = c.Double(nullable: false),
                        Status = c.String(),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductReturnOrders", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.ProductReturnOrders", new[] { "Customer_Id" });
            DropTable("dbo.ProductReturnOrders");
        }
    }
}
