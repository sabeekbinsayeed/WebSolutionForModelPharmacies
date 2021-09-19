namespace WebSolutionForModelPharmacies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Address_Mod : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Addresses", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Addresses", new[] { "Customer_Id" });
            AlterColumn("dbo.Addresses", "Customer_Id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Addresses", "Customer_Id", c => c.Int());
            CreateIndex("dbo.Addresses", "Customer_Id");
            AddForeignKey("dbo.Addresses", "Customer_Id", "dbo.Customers", "Id");
        }
    }
}
