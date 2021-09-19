namespace WebSolutionForModelPharmacies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ticket_Who : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TicketContents", "Who", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TicketContents", "Who");
        }
    }
}
