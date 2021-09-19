using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSolutionForModelPharmacies.Controllers.Admin
{
    public class AdminBaseController : Controller
    {
        public AdminBaseController()
        {
            if (System.Web.HttpContext.Current.Session["Admin"] == null)
            {
                dRedirect.Redirect("/admin/login");
            }
        }
        

    }
}