namespace WebSolutionForModelPharmacies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderReturnsWorking : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductReturns", "ProductReturnOrder_Id", c => c.Int());
            CreateIndex("dbo.ProductReturns", "ProductReturnOrder_Id");
            AddForeignKey("dbo.ProductReturns", "ProductReturnOrder_Id", "dbo.ProductReturnOrders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductReturns", "ProductReturnOrder_Id", "dbo.ProductReturnOrders");
            DropIndex("dbo.ProductReturns", new[] { "ProductReturnOrder_Id" });
            DropColumn("dbo.ProductReturns", "ProductReturnOrder_Id");
        }
    }
}
