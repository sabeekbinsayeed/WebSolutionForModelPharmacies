using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSolutionForModelPharmacies.Models;
using ViewModels;

namespace WebSolutionForModelPharmacies.Controllers.Admin
{
    public class POSController : AdminBaseController
    {


        // GET: POS
        [Route("admin/pos")]
        public ActionResult Index()
        {


            POSViewModel posViewModel = new POSViewModel()
            {
                Product = Database.getContext().Product.ToList(),
                Pricing = Database.getContext().Pricing.ToList(),
            };


            return View("~/Views/Admin/POS/Index.cshtml", posViewModel);
        }
        [Route("admin/pos/cart")]
        public ActionResult PosCart()
        {
            int productId = Convert.ToInt32(Request["productId"]);
            int quantity = Convert.ToInt32(Request["quantity"]);
            int pricingId = Convert.ToInt32(Request["priceId"]);

            dCart.Add(productId, quantity, pricingId, "pos_cart");
            return RedirectToAction("Index", "POS");
        }   

        
        [Route("admin/pos/cart/view")]
        public ActionResult POSCartView()
        {

            CartViewModel cartView = new CartViewModel();
            if (dCart.Check("pos_cart") != null)
            {

                cartView.CartItems = dCart.Get("pos_cart");

                /*foreach (var carti in cartView.CartItems)
                {
                    Response.Write(carti.Product.Title + " ");
                }*/
                return View("~/Views/Admin/POS/POSCart.cshtml", cartView);
            };

            

            return Content("empty ");

            /*
            List<CartItem> cartX = dCart.Get("pos_cart");
            if (cartX == null){
                return Content("Cart is empty");
            }
            //cartX.ForEach(m => Response.Write(m.Product.Title+ m.Cart_Product_Quantity+m.Pricing.Title+"\n"));

            POSViewModel posViewModel = new POSViewModel()
            {
                Product = Database.getContext().Product.ToList(),
                Pricing = Database.getContext().Pricing.Include("Product").ToList(),
            };

            */
            //return View("~/Views/Admin/POS/POSCart.cshtml", cartX);
        }


        [Route("admin/cart/order/place")]
        public ActionResult POSCartOrderPlace()
        {
            double total_stock_price = 0;
            double total_sale_price = 0;

            List<CartItem> cart = dCart.Get("pos_cart");

            List<OrderItem> orderItem = new List<OrderItem>();
            


            Order order = new Order()
            {
                Customer = null,
                Order_type = 1,
                OrderDateTime = System.DateTime.Now,
                PaymentMethod = null,

            };

            foreach (CartItem car in cart){
                orderItem.Add(
                    new OrderItem() {
                        Order = order,
                        Product = car.Product,
                        qty = car.Cart_Product_Quantity,
                        pricing_title = car.Pricing.Title,
                        stock_price = car.Pricing.stock_price,
                        sale_price = car.Pricing.sale_price
                    });
            }


            foreach (OrderItem oitem in orderItem)
            {
                total_stock_price += oitem.stock_price;
                total_sale_price += oitem.sale_price;
            }

            order.total_stock_price = total_stock_price;
            order.total_sale_price = total_sale_price;

            Database.getContext().Order.Add(order);
            Database.getContext().SaveChanges();

            orderItem.ForEach(n => Database.getContext().OrderItem.Add(n));
            Database.getContext().SaveChanges();

            System.Web.HttpContext.Current.Session["pos_cart"] = null;
            System.Web.HttpContext.Current.Session["PosOrderSuccess"] = "PosOrderSuccess";

            return RedirectToAction("Index");
            //return Content(" order done ");

            return View("~/Views/Admin/POS/POSCart.cshtml");
        }

    }
}