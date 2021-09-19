using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSolutionForModelPharmacies.Models;

namespace ViewModel
{
    public class TicketViewModel
    {
        public Ticket Ticket { get; set; }
        public List<TicketContent> TicketContent { get; set; }
    }
}