using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace WebSolutionForModelPharmacies.Models
{
    public class DatabaseContext : DbContext
    {
        
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Customer> Customer { get; set; }
        //public DbSet<Image> Image { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Pricing> Pricing { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<ProductReturn> ProductReturn { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<OrderStatusType> OrderStatusType { get; set; }
        public DbSet<ReturnProduct> ReturnProduct { get; set; }
        public DbSet<ProductReturnOrder> ProductReturnOrder { get; set; }



        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<TicketContent> TicketContent { get; set; }
                

    }
}