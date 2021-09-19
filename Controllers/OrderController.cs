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

    interface IOrderController
    {
        ActionResult AddressPaymentSelect();
        ActionResult PlaceOrder(FormCollection fcollection);
        ActionResult Process();
    }


    public class OrderController : BaseController, IOrderController
    {

        Customer customer;

        public OrderController()
        {
            if ((Customer)System.Web.HttpContext.Current.Session["Customer"] != null)
            {
                customer = (Customer)System.Web.HttpContext.Current.Session["Customer"];
            }
            else
            {
                customer = new Customer();
                dRedirect.Redirect("/authentication");
            }

        }

        /*        
         *        [Route("Order/AddressPaymentSelect")]
        */

        [Route("order/next")]

        public ActionResult AddressPaymentSelect(){


            if ((Customer)Session["Customer"] != null)
            {
                Customer customer = (Customer)Session["Customer"];
                //List<CartItem> cartProducts = (List<CartItem>) Session["cart"];
                List<Address> address = Database.getContext().Address.Where(c => c.Customer_Id == customer.Id).ToList();
                List<PaymentMethod> payment = Database.getContext().PaymentMethod.ToList();

                OrderViewModel order = new OrderViewModel()
                {
                    Customer = customer,
                    Address = address,
                    PaymentMethod = payment,
                    //Products = cartProducts,
                };

                return View("~/Views/Order/AuthenticatedAddressPaymentSelect.cshtml", order);
            }
            else
            {
                Customer customer = Database.getContext().Customer.FirstOrDefault(c => c.Id == 1);
                //List<CartItem> cartProducts = (List<CartItem>) Session["cart"];
                List<Address> address = Database.getContext().Address.Where(c => c.Customer_Id == customer.Id).ToList();
                List<PaymentMethod> payment = Database.getContext().PaymentMethod.ToList();
                OrderViewModel order = new OrderViewModel()
                {
                    Customer = customer,
                    Address = address,
                    PaymentMethod = payment,
                    //Products = cartProducts,
                };

                return View("~/Views/Order/AuthenticatedAddressPaymentSelect.cshtml", order);
            }



        }

        [HttpPost]
        [Route("order/place")]
        public ActionResult PlaceOrder(FormCollection fcollection)
        {
            Customer customer = (Customer)Session["Customer"];
            Address address = null;
            if (Convert.ToInt32(Request["selectedAddress"]) == 0  )
            {
                Address addAddress = new Address()
                {
                    Customer_Id = customer.Id,
                    City = Request["city"],
                    Zip = Convert.ToInt32(Request["zip"]),
                    Details = Request["details"],
                };

                Database.getContext().Address.Add(addAddress);
                Database.getContext().SaveChanges();
                address = addAddress;
            }
            else
            {
                int selectedAddressId = Convert.ToInt32(Request["selectedAddress"]);
                address = Database.getContext().Address.FirstOrDefault(c => c.Id == selectedAddressId);
            }

            PaymentMethod payment = new PaymentMethod();

            if ( Convert.ToInt32(Request["paymentType"]) == 0 )
            {
                payment = new PaymentMethod() {
                    Customer = customer,
                    Type = "Cash On Delivery",
                    SecurityCode = 0
                };
                Database.getContext().PaymentMethod.Add(payment);
                Database.getContext().SaveChanges();
            }
            else
            {
                string date = Request["ExpirationDate"];
                payment = new PaymentMethod()
                {
                    Customer = customer,
                    Type = "Pay Using Card",
                    NameOnCard = Request["NameOnCard"],
                    CardNumber = Request["CardNumber"],
                    ExpirationDate = date,
                    BillingAddress = Request["BillingAddress"],
                    SecurityCode = Convert.ToInt32(Request["SecurityCode"]),
                };

                Database.getContext().PaymentMethod.Add(payment);
                Database.getContext().SaveChanges();

            }

            List<CartItem> cart = (List<CartItem>)Session["Cart"];

            List<OrderItem> orderItem = new List<OrderItem>();

            double total_stock = 0.00;
            double total_sale = 0.00;

            foreach (CartItem car in cart)
            {
                total_stock += car.Cart_Product_Quantity*car.Pricing.stock_price;
                total_sale += car.Cart_Product_Quantity * car.Pricing.sale_price;
            }

            Order order = new Order()
            {
                Customer = customer,
                Order_type = 0,
                City = address.City,
                Zip = address.Zip,
                Street = address.Details,
                OrderDateTime = System.DateTime.Now,
                PaymentMethod = payment,
                total_stock_price = total_stock,
                total_sale_price = total_sale

            };
            Database.getContext().Order.Add(order);
            Database.getContext().SaveChanges();

            foreach (CartItem car in cart){
                orderItem.Add(new OrderItem() { Order = order, Product = car.Product, qty = car.Cart_Product_Quantity, pricing_title = car.Pricing.Title, stock_price = car.Pricing.stock_price, sale_price = car.Pricing.sale_price });
            }
            orderItem.ForEach(n => Database.getContext().OrderItem.Add(n));
            Database.getContext().SaveChanges();


            /*decrease qty*/
            foreach (var decqty in orderItem)
            {
                Product pros = Database.getContext().Product.SingleOrDefault(c => c.Id == decqty.Product.Id);
                pros.qty_stock -= decqty.qty;
                //return Content("dip" + decqty.qty.ToString());
                Database.getContext().SaveChanges();
            }
            /*decrease qty*/



            OrderStatus os = new OrderStatus()
            {
                //OrderStatusType = new OrderStatusType() { Title = "Pending" },
                OrderStatusType = Database.getContext().OrderStatusType.SingleOrDefault(x => x.Title == "Pending"),
                Date = System.DateTime.Now.ToString(),
                Order = order

            };

            ((List<CartItem>)Session["cart"]).Clear();

            Session["cart"] = null;



            Database.getContext().OrderStatus.Add(os);
            Session["OrderPlaceSuccess"] = "OrderPlaceSuccess";
            return RedirectToAction("Index","Home");
            //return View("~/Views/Order/AuthenticatedAddressPaymentSelect.cshtml");
        }

        // GET: Order        
        [Route("order/process")]
        public ActionResult Process()
        {


            List<CartItem> cart = (List<CartItem>)Session["cart"];

            double stock_price_total = 0.00;
            double sale_price_total = 0.00;


            if ((Customer)Session["Customer"] != null)
            {

                //Customer customer = Database.getContext().Customer.SingleOrDefault(c => c.Name == "Kazi");
                Customer customer = (Customer)Session["Customer"];

                Order order = new Order()
                {
                    Customer = customer,
                    //status = "Process",
                    total_stock_price = stock_price_total,
                    total_sale_price = sale_price_total,
                    OrderDateTime = DateTime.Now,
                };
                Database.getContext().Order.Add(order);
                Database.getContext().SaveChanges();

                //return Content(order.Id.ToString() + " " + order.total_stock_price.ToString());

                List<OrderItem> orderItem = new List<OrderItem>();

                foreach (CartItem car in cart)
                {
                    orderItem.Add(new OrderItem() { Order = order, Product = car.Product, qty = car.Cart_Product_Quantity, pricing_title = car.Pricing.Title, stock_price = car.Pricing.stock_price, sale_price = car.Pricing.sale_price });
                }

                orderItem.ForEach(n => Database.getContext().OrderItem.Add(n));

                Database.getContext().SaveChanges();








                /* decrease qty */


                /*
                foreach (var decqty in orderItem)
                {
                    Product pros = Database.getContext().Product.SingleOrDefault(c => c.Id == decqty.Product.Id);
                    pros.qty_stock -= decqty.qty;
                    return Content("dip"+decqty.qty.ToString());
                    Database.getContext().SaveChanges();
                }
                */


                /* decrease qty */











                //Session["cart"] = null;


                ((List<CartItem>)Session["cart"]).Clear();

                Session["cart"] = null;


                return View();
            }

            return Content("Unregistered customer");
            //return Content(cart[0].Product.Title.ToString());
        }
    }
}