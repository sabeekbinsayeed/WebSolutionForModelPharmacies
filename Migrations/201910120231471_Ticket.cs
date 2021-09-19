namespace WebSolutionForModelPharmacies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ticket : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Customer_Email = c.String(),
                        Admin_Id = c.Int(nullable: false),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.TicketContents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Ticket_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tickets", t => t.Ticket_Id)
                .Index(t => t.Ticket_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TicketContents", "Ticket_Id", "dbo.Tickets");
            DropForeignKey("dbo.Tickets", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.TicketContents", new[] { "Ticket_Id" });
            DropIndex("dbo.Tickets", new[] { "Customer_Id" });
            DropTable("dbo.TicketContents");
            DropTable("dbo.Tickets");
        }
    }
}
