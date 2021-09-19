using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using WebSolutionForModelPharmacies.Models;

namespace WebSolutionForModelPharmacies.Controllers.Admin
{
    public class DashboardController : AdminBaseController
    {

        public DashboardController()
        {


        }

        // GET: Dashboard
        [Route("admin/dashboard")] 
        public ActionResult Index()
        {


            List<Product> products = Database.getContext().Product.OrderByDescending(x => x.Id).ToList();

            List<Order> ord = Database.getContext().Order.ToList();
            double totalSalePrice = 0.00;
            double totalStockPrice = 0.00;
            foreach (Order or in ord)
            {
                totalSalePrice  += or.total_sale_price;
                totalStockPrice += or.total_stock_price;
            }
            double revenue = totalSalePrice - totalStockPrice;

            AdminDashboardViewModel advm = new AdminDashboardViewModel()
            {
                TotalProduct = products.Count(),
                TotalCustomer = Database.getContext().Customer.ToList().Count(),
                TotalOrder = Database.getContext().Order.ToList().Count(),
                TotalRevenue = revenue,
                Product  =   products.Take(10).ToList(),
                Supplier =   Database.getContext().Supplier.ToList(),
                Brand    =   Database.getContext().Brand.ToList(),
                Category =   Database.getContext().Category.ToList(),
                Pricing  =   Database.getContext().Pricing.ToList(),

            };

            return View("~/Views/Admin/Dashboard/Index.cshtml", advm);
        }
        

    }
}