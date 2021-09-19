using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSolutionForModelPharmacies.Models;

namespace ViewModels
{
    public class AdminDashboardViewModel
    {
        public List<Product> Product { get; set; }
        public List<Supplier> Supplier { get; set; }
        public List<Brand> Brand { get; set; }
        public List<Category> Category { get; set; }
        public List<Pricing> Pricing { get; set; }
        public int TotalProduct { get; set; }
        public int TotalCustomer { get; set; }
        public int TotalOrder { get; set; }
        public double TotalRevenue { get; set; }

    }
}