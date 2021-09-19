using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSolutionForModelPharmacies.Models;

namespace WebSolutionForModelPharmacies.Controllers.Admin
{
    public class SettingController : BaseController
    {

        [Route("admin/settings")]
        public ActionResult settings()
        {
            return View("~/Views/Admin/Settings/Dashobard.cshtml");
        }

        [Route("admin/password/change")]
        [HttpPost]
        public ActionResult settingPost()
        {
            WebSolutionForModelPharmacies.Models.Admin ad = Database.getContext().Admin.FirstOrDefault();
            ad.Password = Request["password"];
            Database.getContext().SaveChanges();
            Session["AdminPasswordChange"] = "AdminPasswordChange";
            return RedirectToAction("settings");
        }

    }
}