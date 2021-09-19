using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSolutionForModelPharmacies.Models
{
    public class Pricing
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Product Product { get; set; }
        public double stock_price { get; set; }
        public double sale_price { get; set; }

    }
}