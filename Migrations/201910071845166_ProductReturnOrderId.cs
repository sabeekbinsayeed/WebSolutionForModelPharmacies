namespace WebSolutionForModelPharmacies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductReturnOrderId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductReturns", "ProRetOrderId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductReturns", "ProRetOrderId");
        }
    }
}
