using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebSolutionForModelPharmacies.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public string OrderStatus { get; set; }
        public int Order_type { get; set; }
        public double total_stock_price { get; set; }
        public double total_sale_price { get; set; }
        public DateTime OrderDateTime { get; set; }
        //public OrderStatus OrderStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string City { get; set; }
        public int Zip { get; set; }
        public string Street { get; set; }
        public string OrderStatusDate { get; set; }

        //public List<OrderStatus>  OrderStatusCollection { get; set; }


    }
}