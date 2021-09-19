using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSolutionForModelPharmacies.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int parent_id { get; set; }

    }
}