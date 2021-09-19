namespace WebSolutionForModelPharmacies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Address_Modification : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Addresses", "Customer_Id", c => c.Int());
            CreateIndex("dbo.Addresses", "Customer_Id");
            AddForeignKey("dbo.Addresses", "Customer_Id", "dbo.Customers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Addresses", new[] { "Customer_Id" });
            AlterColumn("dbo.Addresses", "Customer_Id", c => c.Int(nullable: false));
        }
    }
}
