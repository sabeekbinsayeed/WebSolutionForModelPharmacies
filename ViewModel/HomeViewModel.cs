using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSolutionForModelPharmacies.Models;

namespace ViewModels
{
    public class HomeViewModel
    {
        public List<Category> Category { get; set; }
        public List<Product> Product { get; set; }
        public List<Image> Image { get; set; }
        public Customer Customer { get; set; }
        public List<Pricing> Price { get; set; }
        public String PrescriptionPath { get; set; }



    }
}