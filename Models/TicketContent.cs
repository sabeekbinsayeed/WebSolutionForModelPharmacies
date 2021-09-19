using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSolutionForModelPharmacies.Models
{
    public class TicketContent
    {

        public int Id { get; set; }
        public Ticket Ticket { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        public string Who { get; set; }

    }
}