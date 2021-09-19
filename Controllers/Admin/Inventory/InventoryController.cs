using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using WebSolutionForModelPharmacies.Models;

namespace WebSolutionForModelPharmacies.Controllers.Admin.Inventory
{
    public class InventoryController : AdminBaseController
    {


        [Route("admin/inventory/order")]
        public ActionResult InventoryOrder()
        {

            InventoryOrderDashboard iorders = new InventoryOrderDashboard()
            {
                Orders = Database.getContext().Order.ToList(),
                Customers = Database.getContext().Customer.ToList(),
                OrderStatus = Database.getContext().OrderStatus.ToList(),
                OrderStatusType = Database.getContext().OrderStatusType.ToList(),
            };


            return View("~/Views/Admin/Inventory/Order/Order_Dashboard.cshtml", iorders);
        }


        [Route("admin/inventory/order/{orderId}/product")]
        public ActionResult OrderProducts(int orderId)
        {

            Order order =  Database.getContext().Order.SingleOrDefault(c => c.Id == orderId);

            List<OrderItem> allOrderItems = Database.getContext().OrderItem.ToList();

            List<OrderItem> orderItems = allOrderItems.Where(c => c.Order == order).ToList();


            InventoryOrderProduct iOP = new InventoryOrderProduct()
            {
                OrderItems = orderItems
            };
            //orderItems.ForEach(s => Response.Write(s.Product.Title));

            //return Content("");

            return View("~/Views/Admin/Inventory/Order/Order_Item.cshtml", iOP);
        }

        
        [Route("admin/inventory/order/{orderId}/status/{statusId}/update")]
        public ActionResult OrderStatusUpdate(int orderId,int statusId)
        {
            var order = Database.getContext().Order.SingleOrDefault(m => m.Id == orderId);

            var status = Database.getContext().OrderStatusType.SingleOrDefault(m => m.Id == statusId);

            //var orderstatus = Database.getContext().OrderStatus.SingleOrDefault(m => m.Id == statusId);

            order.OrderStatus = status.Title;
            order.OrderStatusDate = Convert.ToString(System.DateTime.Now);

            Database.getContext().SaveChanges();

            OrderStatus os = new OrderStatus() {
                OrderStatusType = status,
                Date = Convert.ToString(System.DateTime.Now),
                Order = order
            };
            Database.getContext().OrderStatus.Add(os);
            Database.getContext().SaveChanges();

            return RedirectToAction("InventoryOrder", "Inventory");
        }
        
        [Route("admin/inventory/order/{orderId}/orderitems")]
        public ActionResult OrderItems(int orderId)
        {

            var Order = Database.getContext().Order.SingleOrDefault(m => m.Id == orderId);

            List<OrderItem> orderItemsAll = Database.getContext().OrderItem.ToList();
            List<OrderItem> orderItems = orderItemsAll.Where(c => c.Order == Order).ToList();

            //orderItems.ForEach(m => Response.Write(m.Product.Title));

            //return Content("");

            return View("~/Views/Admin/Inventory/Order/Order_Item.cshtml", orderItems);
        }


          /*      
        [Route("admin/inventory/stockmanager")]
        public ActionResult StockManagement(int orderId)
        {

            var Order = Database.getContext().Order.SingleOrDefault(m => m.Id == orderId);

            List<OrderItem> orderItemsAll = Database.getContext().OrderItem.ToList();
            List<OrderItem> orderItems = orderItemsAll.Where(c => c.Order == Order).ToList();

            //orderItems.ForEach(m => Response.Write(m.Product.Title));

            //return Content("");

            return View("~/Views/Admin/Inventory/Order/Order_Item.cshtml", orderItems);
        }*/


            [Route("admin/return/order/dashboard")]
        public ActionResult ProductRet()
        {
            List<ProductReturnOrder> pro = Database.getContext().ProductReturnOrder.Include("Customer").ToList();

            //pro.ForEach(c => Response.Write(c.Id+"<br>"));
            //return Content("dd");
            return View("~/Views/Admin/Inventory/Return/Return_Order.cshtml", pro);

        }
        
        [Route("admin/inventory/order/{order_id}/status/reject")]
        public ActionResult returnStatusReject(int order_id)
        {
            ProductReturnOrder pro = Database.getContext().ProductReturnOrder.SingleOrDefault( c => c.Id == order_id);
            pro.Status = "Rejected";
            Database.getContext().SaveChanges();

            return RedirectToAction("ProductRet");

            return View("~/Views/Admin/Inventory/Return/Return_Order.cshtml", pro);

        }
        
        [Route("admin/inventory/order/{order_id}/status/accept")]
        public ActionResult returnStatusAccept(int order_id)
        {
            ProductReturnOrder pro = Database.getContext().ProductReturnOrder.SingleOrDefault( c => c.Id == order_id);
            pro.Status = "Accepted";
            Database.getContext().SaveChanges();

            return RedirectToAction("ProductRet");

            //return View("~/Views/Admin/Inventory/Return/Return_Order.cshtml", pro);

        }
                
        [Route("admin/inventory/return_order/{order_id}/products")]
        public ActionResult ReturnOrderProducts(int order_id)
        {

            //ProductReturnOrder prod = Database.getContext().ProductReturnOrder.SingleOrDefault(c => c.Id == order_id);
            List<ProductReturn> pro = Database.getContext().ProductReturn.Include("OrderItem").Where(c => c.ProRetOrderId == order_id).ToList();

            pro.ForEach(c => Response.Write(c.OrderItem.Product.Title));
            pro.ForEach(c => Response.Write(c.Quantity));

            //pro.Status = "Accepted";
            //Database.getContext().SaveChanges();

            //return Content("ProductRet");
            //return RedirectToAction("ProductRet");

            return View("~/Views/Admin/Inventory/Return/Return_Order_Item.cshtml", pro);

        }




        [Route("admin/inventory/stockManagement")]
        public ActionResult stockManagement()
        {

            List<Product> products = Database.getContext().Product.OrderByDescending(x => x.qty_stock).ToList();
            products.Reverse();
            //List<Product> product = products.Reverse();

            AdminDashboardViewModel advm = new AdminDashboardViewModel()
            {
                Product = products,
                Supplier = Database.getContext().Supplier.ToList(),
                Brand = Database.getContext().Brand.ToList(),
                Category = Database.getContext().Category.ToList(),
                Pricing = Database.getContext().Pricing.ToList(),

            };

            return View("~/Views/Admin/Inventory/Stock/StockDashboard.cshtml", advm);
        }



        
        [Route("admin/stock/edit/{product_id}/{qty}")]
        public ActionResult stockUpdate(int product_id,int qty)
        {

            Product products = Database.getContext().Product.SingleOrDefault(x => x.Id == product_id);

            products.qty_stock = qty;

            Database.getContext().SaveChanges();

            return RedirectToAction("stockManagement");

        }

                
        [Route("admin/inventory/prescription")]
        public ActionResult prescriptionDashboard()
        {

            List<Prescription> pres = Database.getContext().Prescription.ToList();

            return View("~/Views/Admin/Inventory/Prescription/Dashboard.cshtml", pres);

        }



















    }






}