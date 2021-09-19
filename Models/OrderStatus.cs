using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSolutionForModelPharmacies.Models
{
    public class OrderStatus
    {
        public int Id { get; set; }
        public OrderStatusType OrderStatusType { get; set; }
        public string Date { get; set; }
        public Order  Order { get; set; }
    }
}