using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSolutionForModelPharmacies.Models
{
    public class Admin
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string _token { get; set; }

    }
}