using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSolutionForModelPharmacies.Models
{
    public class Prescription
    {
        public int Id { get; set; }
        public Customer Cutomer { get; set; }
        public string PrescriptionImg { get; set; }
        public string Status { get; set; }
    }
}