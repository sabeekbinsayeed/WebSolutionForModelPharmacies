using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSolutionForModelPharmacies.Models;

namespace ViewModels
{
    public class OrderViewModel
    {
        public List<Address> Address { get; set; }
        public Customer Customer { get; set; }
        public List<PaymentMethod> PaymentMethod { get; set; }

    }
}