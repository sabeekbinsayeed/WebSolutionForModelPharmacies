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
    public class CartController : BaseController
    {

        [Route("cart/")]
        public ActionResult Index()
        {
            CartViewModel cartView = new CartViewModel();
            if (dCart.Check("cart") != null){
                cartView.CartItems = dCart.Get("cart");
            };


            ViewBag.Cart = (List<CartItem>)System.Web.HttpContext.Current.Session["cart"];
            ViewBag.CartCount = ((List<CartItem>)System.Web.HttpContext.Current.Session["cart"]).Count();

            //return Content("");
            return View(cartView);

        }

        [HttpPost]
        [Route("cart/add")]
        public ActionResult Add()
        {
            int productId =  Convert.ToInt32(Request["productId"]);
            int quantity  =  Convert.ToInt32(Request["quantity"]);
            int pricingId =  Convert.ToInt32(Request["priceId"]);
            dCart.Add(productId, quantity, pricingId, "cart");
            return RedirectToAction("Index");
        }
        

        [HttpGet]
        [Route("cart/increaseQuantity/{productId}/{quantity}/{pricingId}")]
        public ActionResult IncreaseQuantity(int productId, int quantity, int pricingId)
        {
            dCart.IncreaseQuantity(productId, quantity, pricingId, "cart");
            return RedirectToAction("Index");
        }

        [Route("cart/remove/{cartId}")]
        public ActionResult Remove(string cartId)
        {
            dCart.Remove(cartId, "cart");

            return RedirectToAction("Index");
        }

        [Route("cart/removeQty/{cartId}/{qty}")]
        public ActionResult removeQty(string cartId, int qty)
        {

            dCart.RemoveQty(cartId, qty, "cart");

            return RedirectToAction("Index");
        }


       



    }


}