using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSolutionForModelPharmacies.Models;

namespace ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public List<Pricing> Price { get; set; }
        public List<Product> RelatedProducts { get; set; }

    }
}