namespace WebSolutionForModelPharmacies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class order_status_date : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderStatusDate", c => c.String());
            DropColumn("dbo.Orders", "ShippingDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "ShippingDate", c => c.String());
            DropColumn("dbo.Orders", "OrderStatusDate");
        }
    }
}
