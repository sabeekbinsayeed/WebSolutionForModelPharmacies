namespace WebSolutionForModelPharmacies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order_Shipment_Date : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "ShippingDate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "ShippingDate");
        }
    }
}
