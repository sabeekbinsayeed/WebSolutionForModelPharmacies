using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSolutionForModelPharmacies.Controllers.Admin.Account
{
    public class AccountController : AdminBaseController
    {
        public AccountController()
        {

           

        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
    }
}