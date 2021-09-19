using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using WebSolutionForModelPharmacies.Models;

namespace WebSolutionForModelPharmacies.Controllers
{
    public class BaseController : Controller
    {
        public BaseController(){

            List<Category> categories = Database.getContext().Category.ToList();
            List<Brand> brands = Database.getContext().Brand.ToList();
            ViewBag.Category = categories;
            ViewBag.Brand = brands;

            ViewBag.Cart = (List<CartItem>)System.Web.HttpContext.Current.Session["cart"];

            if ((List<CartItem>)System.Web.HttpContext.Current.Session["cart"]!=null)
            {
                ViewBag.CartCount = ((List<CartItem>)System.Web.HttpContext.Current.Session["cart"]).Count();
            }
            else
            {
                ViewBag.CartCount = 0;
            }


        }


    }
}