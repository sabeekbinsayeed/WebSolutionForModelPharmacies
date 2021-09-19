using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSolutionForModelPharmacies.Models;

namespace ViewModels
{
    public class AdminProductViewModel
    {

        public List<Supplier> Supplier { get; set; }
        public List<Brand> Brand { get; set; }
        public List<Category> Category { get; set; }
    }
}