using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSolutionForModelPharmacies.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public string sci_name { get; set; }
        public int qty_stock { get; set; }
        public int SupplierId { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public string ExpiryDate  { get; set; }
    }

}