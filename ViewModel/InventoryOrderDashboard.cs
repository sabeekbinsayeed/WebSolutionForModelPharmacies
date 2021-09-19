using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSolutionForModelPharmacies.Models;

namespace ViewModels
{
    public class InventoryOrderDashboard
    {
        public List<Order> Orders { get; set; }
        public List<OrderStatus> OrderStatus { get; set; }
        public List<Customer> Customers { get; set; }
        public List<OrderStatusType> OrderStatusType { get; set; }
    }
}