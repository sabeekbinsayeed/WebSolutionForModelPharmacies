namespace WebSolutionForModelPharmacies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Payment_Date : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PaymentMethods", "ExpirationDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PaymentMethods", "ExpirationDate", c => c.DateTime(nullable: false));
        }
    }
}
