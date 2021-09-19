using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSolutionForModelPharmacies.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        public int qty { get; set; }
        public string pricing_title { get; set; }
        public double stock_price { get; set; }
        public double sale_price { get; set; }

    }


}