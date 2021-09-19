namespace WebSolutionForModelPharmacies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ticket_datetime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "DateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "DateTime");
        }
    }
}
