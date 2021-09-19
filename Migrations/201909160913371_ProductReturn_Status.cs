namespace WebSolutionForModelPharmacies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductReturn_Status : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductReturns", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductReturns", "Status");
        }
    }
}
