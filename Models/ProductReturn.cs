using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSolutionForModelPharmacies.Models
{
    public class ProductReturn
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public OrderItem OrderItem { get; set; }
        public ProductReturnOrder ProductReturnOrder { get; set; }
        public int ProRetOrderId { get; set; }
        public double asking_price { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
    }
}