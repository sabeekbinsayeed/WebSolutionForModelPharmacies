using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSolutionForModelPharmacies.Models;

namespace ViewModels
{
    public class POSViewModel
    {
        public List<Product> Product { get; set; }
        public List<Pricing> Pricing { get; set; }
    }
}