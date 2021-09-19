using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSolutionForModelPharmacies.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Customer Customer { get; set; }
        public string Customer_Email { get; set; }
        public int Admin_Id { get; set; }
        public DateTime DateTime { get; set; }

    }
}