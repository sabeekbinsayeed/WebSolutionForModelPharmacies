namespace WebSolutionForModelPharmacies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order_Status_Type : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "OrderStatus_Id", "dbo.OrderStatus");
            DropIndex("dbo.Orders", new[] { "OrderStatus_Id" });
            DropColumn("dbo.Orders", "OrderStatus_Id");
            DropTable("dbo.OrderStatus");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Date = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Orders", "OrderStatus_Id", c => c.Int());
            CreateIndex("dbo.Orders", "OrderStatus_Id");
            AddForeignKey("dbo.Orders", "OrderStatus_Id", "dbo.OrderStatus", "Id");
        }
    }
}
