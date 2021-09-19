using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSolutionForModelPharmacies.Models
{
    public class ReturnProduct
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public OrderItem OrderItem { get; set; }
        public Customer Customer { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int Zip { get; set; }
        public string AskingReturnPrice { get; set; }
        public string ExpiryDate { get; set; }
        public string Status { get; set; }

    }
}