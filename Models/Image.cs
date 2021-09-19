using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSolutionForModelPharmacies.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Path { get; set; }        
        public int ProductId { get; set; }
        
    }
}