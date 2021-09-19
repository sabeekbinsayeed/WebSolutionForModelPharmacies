using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSolutionForModelPharmacies.Models;
using Helper;
using System.Net.Mail;

namespace WebSolutionForModelPharmacies.Controllers
{


    interface IAuthenticationController
    {

        ActionResult Index();
        ActionResult Logout();
        ActionResult Login(FormCollection formCol);
        ActionResult Register();
        ActionResult Address(Customer cust, FormCollection formColct);
        ActionResult EmailVerify(string token);
        ActionResult ResetPassword();
        ActionResult ResetPasswords(string token);
        ActionResult ResetPasswordPostAction();

    }


    partial class AuthenticationController
    {

        AuthenticationController dd = new AuthenticationController();

    }
    partial class AuthenticationController : BaseController , IAuthenticationController
    {

        // GET: Authentication
        [Route("authentication/")]
        public ActionResult Index()
        {
            return View();
        }


        // GET: Authentication
        [HttpGet]
        [Route("authentication/logout")]
        public ActionResult Logout()
        {

            Session["Customer"] = null;

            return RedirectToAction("Index");
            //return View("~/Views/Admin/Authentication/Index.cshtml");
        }


        [HttpPost]
        [Route("authentication/login")]
        public ActionResult Login(FormCollection formCol)
        {
            string email = formCol["email"];
            string password = formCol["password"];
            Customer customer = Database.getContext().Customer.FirstOrDefault(m => m.Email == email);
            if ((customer != null) && (bool)dHash.verify(password, customer.Password))
            {
                Session["Customer"] = customer;
                Session["LoginSuccess"] = "LoginSuccess";
                return RedirectToAction("Index","Home");
            }
            else{
                Session["CustomerError"] = "CustomerError";
            }

            return RedirectToAction("Index", "Authentication");
        }


        [HttpPost]
        [Route("authentication/register")]
        public ActionResult Register()
        {
            string name     = Request["name"];
            string email    = Request["email"];
            string password = Request["password"];
            string phone    = Request["phone"];
            string confirm_password = Request["confirm_password"];
            if (Database.getContext().Customer.SingleOrDefault(m => m.Email == email) == null)
            {
                Customer customer = new Customer()
                {
                    Name = name,
                    Email = email,
                    Password = dHash.make(password),
                    _token = dHash.GetMd5(email),
                    Phone = phone,
                };
                Database.getContext().Customer.Add(customer);
                Database.getContext().SaveChanges();
                ViewBag.email = email;
                //Session["Customer"] = customer;
                //Session["LoginSuccess"] = "LoginSuccess";
                dMailer.EmailVerificationMail(email, dHash.GetMd5(email));
                return View("Address");
            }
            else{
                Session["UserExists"] = true;
                Response.Write("User Already Exists");
                return RedirectToAction("Index");
            }
           
        }

        [HttpPost]
        [Route("authentication/address/register")]
        public ActionResult Address(Customer cust,FormCollection formColct)
        {
            string email = formColct["email"];
            Customer customer = Database.getContext().Customer.SingleOrDefault(m => m.Email == email);

            Address address = new Address()
            {
                Customer_Id = customer.Id,
                City = formColct["city"],
                Zip = Convert.ToInt32(formColct["zip"]),
                Details = formColct["details"],
            };

            Database.getContext().Address.Add(address);
            Database.getContext().SaveChanges();
            Session["RgistrationSuccess"] = "RgistrationSuccess";
            //Session["Customer"] = customer;
            //Session["LoginSuccess"] = "LoginSuccess";
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        [Route("email/verify/{token}")]
        public ActionResult EmailVerify(string token)
        {
            Customer customer = Database.getContext().Customer.FirstOrDefault(m => m._token == token);
            //Response.Write(customer.Name);
            if (customer!=null)
            {
                customer._token = null;
                Database.getContext().SaveChanges();
                Session["EmailVerificationSuccess"] = "EmailVerificationSuccess";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Response.Write("token expired!");
            }
            return Content("Something wrong!");

        }


        [HttpGet]
        [Route("reset/password")]
        public ActionResult ResetPassword()
        {


            return View("~/Views/Authentication/ResetPassword.cshtml");

        }     
        
        [HttpPost]
        [Route("reset/password/post")]
        public ActionResult ResetPasswordPost()
        {


            string email = Request["email"];

            dMailer.PasswordRecoverEmail(email,dHash.GetMd5(email));

            Customer cst = Database.getContext().Customer.SingleOrDefault(c => c.Email == email);

            cst._token = dHash.GetMd5(email);

            Database.getContext().SaveChanges();

            Session["PasswordResetConfirmation"] = "PasswordResetConfirmation";

            return RedirectToAction("Index");

            //return View("~/Views/Admin/Authentication/ResetPassword.cshtml");

        }
                
        [HttpGet]
        [Route("password/recovery/{token}")]
        public ActionResult ResetPasswords(string token)
        {

            Customer cst = Database.getContext().Customer.SingleOrDefault(c => c._token == token);

            PasswordReset pr = new PasswordReset() {
                Email = cst.Email,
                _token = cst._token,
            };
            //string email = Request["email"];

            //dMailer.EmailVerificationMail(email,dHash.GetMd5(email));

            //return Content(email);

            return View("~/Views/Authentication/ResetPasswordConfirmPassword.cshtml", pr);

        }
                        
        [HttpPost]
        [Route("reset/password/confirm/post")]
        public ActionResult ResetPasswordPostAction()
        {
            string email = Request["email"];
            Customer cst = Database.getContext().Customer.SingleOrDefault(c => c.Email == email);

            cst._token = null;
            cst.Password = dHash.make(Request["password"]);

            Database.getContext().SaveChanges();

            Session["PasswordReset"] = "PasswordReset";

            return RedirectToAction("Index");
            //return View("~/Views/Authentication/ResetPasswordConfirmPassword.cshtml", pr);

        }






    }




}
