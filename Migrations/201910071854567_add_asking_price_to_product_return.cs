namespace WebSolutionForModelPharmacies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_asking_price_to_product_return : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductReturns", "asking_price", c => c.Double(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductReturns", "asking_price");
        }
    }
}
