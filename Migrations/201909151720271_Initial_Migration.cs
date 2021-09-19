namespace WebSolutionForModelPharmacies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        City = c.String(),
                        Zip = c.Int(nullable: false),
                        Details = c.String(),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Phone = c.String(),
                        _token = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        _token = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        title = c.String(),
                        details = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        parent_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Order_type = c.Int(nullable: false),
                        Address = c.String(),
                        status = c.String(),
                        total_stock_price = c.Double(nullable: false),
                        total_sale_price = c.Double(nullable: false),
                        OrderDateTime = c.DateTime(nullable: false),
                        Customer_Id = c.Int(),
                        PaymentMethod_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.PaymentMethods", t => t.PaymentMethod_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.PaymentMethod_Id);
            
            CreateTable(
                "dbo.PaymentMethods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        NameOnCard = c.String(),
                        CardNumber = c.String(),
                        ExpirationDate = c.DateTime(nullable: false),
                        BillingAddress = c.String(),
                        SecurityCode = c.Int(nullable: true),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        qty = c.Int(nullable: false),
                        pricing_title = c.String(),
                        stock_price = c.Double(nullable: false),
                        sale_price = c.Double(nullable: false),
                        Order_Id = c.Int(),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Order_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Img = c.String(),
                        sci_name = c.String(),
                        qty_stock = c.Int(nullable: true),
                        SupplierId = c.Int(nullable: true),
                        BrandId = c.Int(nullable: true),
                        CategoryId = c.Int(nullable: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pricings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        stock_price = c.Double(nullable: false),
                        sale_price = c.Double(nullable: false),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Details = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pricings", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.OrderItems", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.OrderItems", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Orders", "PaymentMethod_Id", "dbo.PaymentMethods");
            DropForeignKey("dbo.PaymentMethods", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Orders", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Addresses", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Pricings", new[] { "Product_Id" });
            DropIndex("dbo.OrderItems", new[] { "Product_Id" });
            DropIndex("dbo.OrderItems", new[] { "Order_Id" });
            DropIndex("dbo.PaymentMethods", new[] { "Customer_Id" });
            DropIndex("dbo.Orders", new[] { "PaymentMethod_Id" });
            DropIndex("dbo.Orders", new[] { "Customer_Id" });
            DropIndex("dbo.Addresses", new[] { "Customer_Id" });
            DropTable("dbo.Suppliers");
            DropTable("dbo.Pricings");
            DropTable("dbo.Products");
            DropTable("dbo.OrderItems");
            DropTable("dbo.PaymentMethods");
            DropTable("dbo.Orders");
            DropTable("dbo.Categories");
            DropTable("dbo.Brands");
            DropTable("dbo.Admins");
            DropTable("dbo.Customers");
            DropTable("dbo.Addresses");
        }
    }
}
