using Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using WebSolutionForModelPharmacies.Models;

namespace WebSolutionForModelPharmacies.Controllers
{
    public class ProfileController : BaseController
    {
        Customer customer;
        // constructor not loading
        public ProfileController()
        {
            if ((Customer)System.Web.HttpContext.Current.Session["Customer"] != null){
                customer = (Customer)System.Web.HttpContext.Current.Session["Customer"];
            }
            else{
                customer = new Customer();
                dRedirect.Redirect("/authentication");
            }

        }


        // GET: Profile
        [HttpGet]
        [Route("profile/dashboard")]
        public ActionResult Index()
        {
            List<Order> orders = Database.getContext().Order.ToList();
            List<Order> order = orders.Where(c => c.Customer == customer).ToList();

            List<OrderStatus> orderStatus = Database.getContext().OrderStatus.ToList();
            List<OrderStatusType> orderStatusTypes = Database.getContext().OrderStatusType.ToList();

            ProfileOrderViewModel profileOrderViewModel = new ProfileOrderViewModel()
            {
                Order = order,
                OrderStatus = orderStatus,
                OrderStatusType = orderStatusTypes
            };


            return View(profileOrderViewModel);
        }

        [HttpGet]
        [Route("profile/order/{orderId}/product")]
        public ActionResult OrderProduct(int orderId)
        {
            //Response.Write(orderId.Id);
            Order order = Database.getContext().Order.SingleOrDefault(c=>c.Id == orderId);
            List<OrderItem> orderItem = Database.getContext().OrderItem.Include("Product").Include("Order").ToList();
            List<OrderItem> orderItems = orderItem.Where(c => c.Order == order).ToList();

            ProfileOrderItemViewModel povm = new ProfileOrderItemViewModel()
            {
                OrderItems = orderItems,

            };
            //orderItems.ForEach(m => Response.Write(m.Product.Title+"\n"));
            //return Content("");
            return View("~/Views/Profile/OrderProduct.cshtml", povm);
        }

        [HttpGet]
        [Route("profile/return/{orderId}/product")]
        public ActionResult ReturnProductAdd(int orderId)
        {
            //Response.Write(orderId.Id);
            Order order = Database.getContext().Order.SingleOrDefault(c=>c.Id == orderId);
            List<OrderItem> orderItem = Database.getContext().OrderItem.Include("Product").Include("Order").ToList();
            List<OrderItem> orderItems = orderItem.Where(c => c.Order == order).ToList();

            ProfileOrderItemViewModel povm = new ProfileOrderItemViewModel()
            {
                OrderItems = orderItems,

            };
            //orderItems.ForEach(m => Response.Write(m.Product.Title+"\n"));
            //return Content("");
            return View("~/Views/Profile/ReturnProductAddProduct.cshtml", povm);
        }

        [HttpPost]
        [Route("profile/product/return/cart/add")]
        public ActionResult ReturnProduct()
        {
            int productId = Convert.ToInt32(Request["product_id"]);
            int quantity = Convert.ToInt32(Request["quantity_return"]);
            double askingPrice = Convert.ToDouble(Request["asking_price"]);
            int order_item_id = Convert.ToInt32(Request["order_item_id"]);
            //List<ReturnProductCartModel> cart = new List<ReturnProductCartModel>();
            if (System.Web.HttpContext.Current.Session["return_cart"] == null)
            {
                List<ReturnProductCartModel> cart = new List<ReturnProductCartModel>();
                cart.Add(
                    new ReturnProductCartModel()
                    {
                        Product = Database.getContext().Product.SingleOrDefault(c => c.Id == productId),
                        Qty = quantity,
                        Price = askingPrice,
                        OrderItemId = order_item_id
                    }
                );
                System.Web.HttpContext.Current.Session["return_cart"] = cart;
            }
            else
            {
                List<ReturnProductCartModel> cart = (List<ReturnProductCartModel>) System.Web.HttpContext.Current.Session["return_cart"];
                cart.Add(
                    new ReturnProductCartModel()
                    {
                        Product = Database.getContext().Product.SingleOrDefault(c => c.Id == productId),
                        Qty = quantity,
                        Price = askingPrice,
                        OrderItemId = order_item_id
                    }
                );
                System.Web.HttpContext.Current.Session["return_cart"] = cart;
            }


            System.Web.HttpContext.Current.Session["ReturnCartAdded"] = "Success";

            return RedirectToAction("returnCart");
            return RedirectToAction("OrderProduct", "Profile", new
            {
                orderId = Convert.ToInt32(Request["order_id"])
            });
        }

        [Route("profile/return/cart/dashboard")]
        public ActionResult returnCart(){
            List<ReturnProductCartModel> cartItems = (List<ReturnProductCartModel>)System.Web.HttpContext.Current.Session["return_cart"];
            ReturnCartDashboardModel rcdvm = new ReturnCartDashboardModel(){
                ReturnProductCartModel = cartItems,
                Address = Database.getContext().Address.Where(c => c.Customer_Id == customer.Id).ToList(),
            };
            //cartItems.ForEach(c => Response.Write(c.Product.Title + " " + c.Qty + " " + c.Price));
            return View("~/Views/Profile/ReturnProductCart.cshtml", rcdvm);
        }


        [Route("return/cart/remove/{index}")]
        public ActionResult ReturnCartRemove(int index)
        {
            List<ReturnProductCartModel> cartItems = (List<ReturnProductCartModel>)System.Web.HttpContext.Current.Session["return_cart"];

            //Product pd = Database.getContext().Product.FirstOrDefault(c => c.Id == Product_Id);
            // dipsgalaxy01@gmail.com
            cartItems.RemoveAt(index);

            Session["return_cart"] = cartItems;

            ReturnCartDashboardModel rcdvm = new ReturnCartDashboardModel()
            {
                ReturnProductCartModel = cartItems,
                Address = Database.getContext().Address.Where(c => c.Customer_Id == customer.Id).ToList(),
            };
            //cartItems.ForEach(c => Response.Write(c.Product.Title + " " + c.Qty + " " + c.Price));
            return View("~/Views/Profile/ReturnProductCart.cshtml", rcdvm);
        }


        [Route("profile/product/return/dashboard")]
        public ActionResult ReturnProductDashboard(){
            List<Order> orders = Database.getContext().Order.ToList();
            List<Order> order = orders.Where(c => c.Customer == customer).ToList();
            List<OrderStatus> orderStatus = Database.getContext().OrderStatus.ToList();
            List<OrderStatusType> orderStatusTypes = Database.getContext().OrderStatusType.ToList();
            ProfileOrderViewModel profileOrderViewModel = new ProfileOrderViewModel()
            {
                Order = order,
                OrderStatus = orderStatus,
                OrderStatusType = orderStatusTypes
            };

            return View("~/Views/Profile/ReturnProductDashboard.cshtml", profileOrderViewModel);
        }


        [HttpPost]
        [Route("return/cart/process")]
        public ActionResult ReturnProcess(){

            List<ReturnProductCartModel> cart = (List<ReturnProductCartModel>)System.Web.HttpContext.Current.Session["return_cart"];
            int addressId = Convert.ToInt32(Request["addressId"]);
            ProductReturnOrder productReturnOrder = new ProductReturnOrder(){
                DateTime = System.DateTime.Now,
                Customer = customer,
                Street = Request["street" + addressId],
                Zip = Convert.ToInt32(Request["zip" + addressId]),
                City = Request["city" + addressId],
                TotalAskingPrice = Convert.ToDouble(Request["subTotal"]),
                Status = "Pending"
            };
            ProductReturnOrder AddedProductReturnOrder =  Database.getContext().ProductReturnOrder.Add(productReturnOrder);
            Database.getContext().SaveChanges();

            List<ProductReturn> ProductReturn = new List<ProductReturn>();
            //cart[0].ProductReturnOrder = AddedProductReturnOrder;
            foreach (var car in cart){


            

                ProductReturn proretadd = new ProductReturn{
                    Customer = customer,
                    OrderItem = Database.getContext().OrderItem.SingleOrDefault(c => c.Id == car.OrderItemId),
                    ProductReturnOrder = AddedProductReturnOrder,
                    Quantity = car.Qty,
                    Status = "Pending",

                    ProRetOrderId = AddedProductReturnOrder.Id,

                    asking_price = car.Price

                };

                /*Response.Write(proretadd.Customer.Email+"<br>");
                Response.Write(proretadd.OrderItem.Product.Title+ "<br>");
                Response.Write(proretadd.ProductReturnOrder.Id+ "<br>");
                Response.Write(proretadd.Quantity + "<br>");
                Response.Write(proretadd.ProRetOrderId + "<br>");
                Response.Write(proretadd.asking_price + "<br>");*/


                Database.getContext().ProductReturn.Add(proretadd);
                Database.getContext().SaveChanges();




            }

            //return Content("");

            System.Web.HttpContext.Current.Session["return_cart"] = null;
            System.Web.HttpContext.Current.Session["ReturnCartSuccess"] = "ReturnCartSuccess";
            return RedirectToAction("Index", "Home");

        }


        [HttpGet]
        [Route("profile/upload/prescription")]
        public ActionResult UploadPrescription()
        {
            return View();
        }

        [HttpPost]
        [Route("profile/upload/prescription/process")]
        public ActionResult UploadPrescriptionProcess()
        {
            string fileUrl = dUpload.Upload("upload_prescription", "~/Uploads/prescriptions/");
            Prescription pres = new Prescription()
            {
                Cutomer = Database.getContext().Customer.SingleOrDefault(c => c.Id == 1),
                PrescriptionImg = fileUrl,
                Status = "Pending"
            };
            Database.getContext().Prescription.Add(pres);
            Database.getContext().SaveChanges();

            //return Content(fileUrl);
            System.Web.HttpContext.Current.Session["UploadSuccess"] = "UploadSuccess";
            return RedirectToAction("UploadPrescription");
        }



        /*Profile Address*/

        [HttpGet]
        [Route("profile/address")]
        public ActionResult Address()
        {
            List<Address> addr = Database.getContext().Address.Where(c=> c.Customer_Id == customer.Id).ToList();
            //addr.ForEach(c => Response.Write(c.Details));
            //return Content("");
            return View(addr);
        }
        [HttpPost]
        [Route("profile/add/address")]
        public ActionResult AddAddress()
        {

            Database.getContext().Address.Add(new Address() {
                Customer_Id = customer.Id,
                City = Request["City"],
                Details = Request["Details"],
                Zip = Convert.ToInt32(Request["Zip"])
            }) ;
            Database.getContext().SaveChanges();
            return RedirectToAction("Address");
            //return View();
        }

        [HttpGet]
        [Route("profile/address/{addressId}/delete")]
        public ActionResult DeleteAddress(int addressId)
        {
            Address addr = Database.getContext().Address.SingleOrDefault(c => c.Id == addressId);
            Database.getContext().Address.Remove(addr);
            Database.getContext().SaveChanges();

            return RedirectToAction("Address");
            //return View();
        }





        /*Profile Settings*/
        [HttpGet]
        [Route("profile/settings")]
        public ActionResult Settings()
        {
            //Address addr = Database.getContext().Address.SingleOrDefault(c => c.Id == addressId);
            //Database.getContext().Address.Remove(addr);
            //Database.getContext().SaveChanges();

            //return RedirectToAction("Address");
            return View();
        }
        [HttpPost]
        [Route("profile/change/password")]
        public ActionResult ChangePassword()
        {
            // dipsgalaxy01@gmail.com
            //if (dHash.make(Request["OldPassword"]).Equals(customer.Password) && Request["Password"].Equals(Request["Confirm_Password"]))
            if (  dHash.verify( Request["OldPassword"], customer.Password ) && Request["Password"].Equals(Request["Confirm_Password"]))
            //if (Request["Password"].Equals(Request["Confirm_Password"]))
            {
                Customer cust = Database.getContext().Customer.SingleOrDefault(c => c.Id == customer.Id);
                cust.Password = dHash.make(Request["Password"]);
                Database.getContext().SaveChanges();
                Session["PasswordChange"] = "PasswordChange";
                return RedirectToAction("Settings");

            }
            return Content("Something Wrong");
           // return View();
        }








    }
}