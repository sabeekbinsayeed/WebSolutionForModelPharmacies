using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSolutionForModelPharmacies.Models;

namespace ViewModels
{
    public class ReturnCartDashboardModel
    {
        public List<Address> Address { get; set; }
        public List<ReturnProductCartModel> ReturnProductCartModel { get; set; }

    }
}