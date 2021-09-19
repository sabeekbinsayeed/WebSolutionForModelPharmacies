namespace WebSolutionForModelPharmacies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSomething : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReturnProducts", "OrderItem_Id", c => c.Int());
            CreateIndex("dbo.ReturnProducts", "OrderItem_Id");
            AddForeignKey("dbo.ReturnProducts", "OrderItem_Id", "dbo.OrderItems", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReturnProducts", "OrderItem_Id", "dbo.OrderItems");
            DropIndex("dbo.ReturnProducts", new[] { "OrderItem_Id" });
            DropColumn("dbo.ReturnProducts", "OrderItem_Id");
        }
    }
}
