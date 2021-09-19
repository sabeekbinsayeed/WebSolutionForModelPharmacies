namespace WebSolutionForModelPharmacies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order_Update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "City", c => c.String());
            AddColumn("dbo.Orders", "Zip", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "Street", c => c.String());
            DropColumn("dbo.Orders", "Address");
            DropColumn("dbo.Orders", "status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "status", c => c.String());
            AddColumn("dbo.Orders", "Address", c => c.String());
            DropColumn("dbo.Orders", "Street");
            DropColumn("dbo.Orders", "Zip");
            DropColumn("dbo.Orders", "City");
        }
    }
}
