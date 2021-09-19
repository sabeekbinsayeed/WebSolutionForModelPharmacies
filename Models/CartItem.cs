using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSolutionForModelPharmacies.Models
{
    public class CartItem
    {
        public string CartId { get; set; }
        public Product Product { get; set; }
        public int Cart_Product_Quantity { get; set; }
        //public int Cart_Product_Id { get; set; }
        public Pricing Pricing { get; set; }

    }
}