namespace WebSolutionForModelPharmacies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order_Status_Type2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.String(),
                        Order_Id = c.Int(),
                        OrderStatusType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .ForeignKey("dbo.OrderStatusTypes", t => t.OrderStatusType_Id)
                .Index(t => t.Order_Id)
                .Index(t => t.OrderStatusType_Id);
            
            CreateTable(
                "dbo.OrderStatusTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderStatus", "OrderStatusType_Id", "dbo.OrderStatusTypes");
            DropForeignKey("dbo.OrderStatus", "Order_Id", "dbo.Orders");
            DropIndex("dbo.OrderStatus", new[] { "OrderStatusType_Id" });
            DropIndex("dbo.OrderStatus", new[] { "Order_Id" });
            DropTable("dbo.OrderStatusTypes");
            DropTable("dbo.OrderStatus");
        }
    }
}
