namespace WebSolutionForModelPharmacies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TicketContents", "DateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TicketContents", "DateTime");
        }
    }
}
