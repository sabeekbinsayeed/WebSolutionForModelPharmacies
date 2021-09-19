using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSolutionForModelPharmacies.Models
{
    public class ReturnProductCartModel
    {
        public Product Product { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
        public int OrderItemId { get; set; }

    }
}