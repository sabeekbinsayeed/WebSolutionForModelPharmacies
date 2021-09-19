namespace WebSolutionForModelPharmacies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Address_CustomerId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Addresses", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Addresses", new[] { "CustomerId" });
            AddColumn("dbo.Addresses", "Customer_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Addresses", "CustomerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Addresses", "CustomerId", c => c.Int(nullable: false));
            DropColumn("dbo.Addresses", "Customer_Id");
            CreateIndex("dbo.Addresses", "CustomerId");
            AddForeignKey("dbo.Addresses", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
        }
    }
}
