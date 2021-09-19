using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSolutionForModelPharmacies.Models
{
    public class PasswordReset
    {

        public string Email { get; set; }
        public string _token { get; set; }

    }
}