using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSolutionForModelPharmacies.Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public string Type { get; set; }
        public string NameOnCard { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public String BillingAddress { get; set; }
        public int SecurityCode { get; set; }

    }
}