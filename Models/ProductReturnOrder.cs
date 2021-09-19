using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSolutionForModelPharmacies.Models
{
    public class ProductReturnOrder
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public Customer Customer { get; set; }
        public string Street { get; set; }
        public int Zip { get; set; }
        public string City  { get; set; }
        public double TotalAskingPrice { get; set; }
        public double TotalSalePrice { get; set; }
        public string Status { get; set; }

    }
}