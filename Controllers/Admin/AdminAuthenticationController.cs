using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace WebSolutionForModelPharmacies.Controllers.Admin
{
    public class AdminAuthenticationController : Controller
    {
        public AdminAuthenticationController()
        {
            if (System.Web.HttpContext.Current.Session["Admin"] != null)
            {
                dRedirect.Redirect("/admin/dashboard");
            }

        }

        // GET: Authentication
        [HttpGet]
        [Route("admin/login")]
        public ActionResult Index()
        {
            //return RedirectToAction("Index","Dashboard");
            return View("~/Views/Admin/Authentication/Index.cshtml");
        }        
        

        [HttpPost]
        [Route("admin/login/post")]
        public ActionResult Login()
        {
            string email = Request["email"];
            string password = Request["password"];
            WebSolutionForModelPharmacies.Models.Admin admin = Database.getContext().Admin.SingleOrDefault(c => c.Email == email);
            if (admin != null && admin.Password == password)
            {
                Session["AdminLogin"] = "Success"; 
                Session["Admin"] = admin; 
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                Session["AdminLogin"] = "Failed";
                return RedirectToAction("Index", "AdminAuthentication");
                //return RedirectToAction("rest", "TestCtrl");
                //return RedirectToAction("rest", "TestCtrl", new { area = "Test" });
            }

        }
        [HttpGet]
        [Route("admin/logout")]
        public ActionResult Logout()
        {
            
                
            Session["Admin"] = null; 
                
            return RedirectToAction("Index", "AdminAuthentication");
            

        }


    }
}