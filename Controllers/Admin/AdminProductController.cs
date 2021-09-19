using Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using WebSolutionForModelPharmacies.Models;

namespace WebSolutionForModelPharmacies.Controllers.Admin
{
    public class AdminProductController : AdminBaseController
    {

        public AdminProductController()
        {

         
        }

        // GET: Dashboard
        [HttpGet]
        [Route("admin/product/dashboard")]
        public ActionResult ProductDashboard()
        {
            List<Product> products = Database.getContext().Product.OrderByDescending(x => x.Id).ToList();
            //List<Product> product = products.Reverse();

            AdminDashboardViewModel advm = new AdminDashboardViewModel()
            {
                Product = products,
                Supplier = Database.getContext().Supplier.ToList(),
                Brand = Database.getContext().Brand.ToList(),
                Category = Database.getContext().Category.ToList(),
                Pricing = Database.getContext().Pricing.ToList(),

            };

            return View("~/Views/Admin/Product/Index.cshtml", advm);
        }

        [Route("admin/product/edit/{productId}")]
        public ActionResult Edit(int productId)
        {
            Product product = Database.getContext().Product.SingleOrDefault(c => c.Id == productId);
            List<Pricing> pricing = Database.getContext().Pricing.ToList();
            List<Supplier> supplier = Database.getContext().Supplier.ToList();
            List<Category> category = Database.getContext().Category.ToList();
            List<Brand> brand = Database.getContext().Brand.ToList();

            AdminProductEditModel adminProductEditModel = new AdminProductEditModel()
            {
                Product = product,
                Pricing = pricing,
                Supplier = supplier,
                Category = category,
                Brand = brand,
            };
            return View("~/Views/Admin/Product/Edit.cshtml", adminProductEditModel);
        }


        [HttpPost]
        [Route("admin/product/edit/process")]
        public ActionResult EditProcess()
        {

            int productId = Convert.ToInt32(Request["product_id"]);
            Product product = Database.getContext().Product.SingleOrDefault(c => c.Id == productId);
            List<Pricing> _pricing = Database.getContext().Pricing.ToList();
            List<Pricing> pricing = _pricing.Where(c => c.Product == product).ToList();

            //int i = 0;
            foreach (var pr in pricing)
            {
                //pr.Title = Request["title"];
                //pr.stock_price = Request["description"];
                //string title = "old_pricing_id_0_" + pr.Id;
                string _title = "old_pricing_title_0_" + pr.Id;
                string _stock = "old_stock_price_0_" + pr.Id;
                string _sale = "old_sale_price_0_" + pr.Id;

                pr.Title = Convert.ToString(Request[_title]);
                pr.stock_price = Convert.ToDouble(Request[_stock]);
                pr.sale_price = Convert.ToDouble(Request[_sale]);

                Database.getContext().SaveChanges();

            }


            HttpPostedFileBase file = Request.Files["fileAbsolute"];
            string path = "";
            if (file != null && file.ContentLength > 0)
            {
                path = dUpload.Upload("fileAbsolute","~/Uploads/");
            }
            else
            {
                path = product.Img;
            }


            product.Title = Convert.ToString(Request["title"]);
            product.Description = Convert.ToString(Request["description"]);
            product.qty_stock = Convert.ToInt32(Request["qty_stock"]);
            product.sci_name = Convert.ToString(Request["sci_name"]);
            product.Img = path;
            product.SupplierId = Convert.ToInt32(Request["supplier"]);
            product.BrandId = Convert.ToInt32(Request["brand"]);
            product.CategoryId = Convert.ToInt32(Request["category"]);
            product.ExpiryDate = Convert.ToString(Request["expiryDate"]);

            Database.getContext().SaveChanges();

            if (Request["noOfPricing"] != null)
            {
                int noOf = Convert.ToInt32(Request["noOfPricing"]);

                for (int i = 0; i < noOf; i++)
                {
                    string _pricing_title = "pricing_title" + i;
                    string _stock_price = "stock_price" + i;
                    string _sale_price = "stock_price" + i;

                    string pricing_title = Convert.ToString(Request[_pricing_title]);
                    double stock_price = Convert.ToDouble(Request[_stock_price]);
                    double sale_price = Convert.ToDouble(Request[_sale_price]);

                    Pricing __pricing = new Pricing()
                    {
                        Title = pricing_title,
                        Product = product,
                        stock_price = stock_price,
                        sale_price = sale_price
                    };

                    Database.getContext().Pricing.Add(__pricing);
                    Database.getContext().SaveChanges();

                }
            }
            


            List<Supplier> supplier = Database.getContext().Supplier.ToList();
            List<Category> category = Database.getContext().Category.ToList();
            List<Brand> brand = Database.getContext().Brand.ToList();

            AdminProductEditModel adminProductEditModel = new AdminProductEditModel()
            {
                Product = product,
                Pricing = pricing,
                Supplier = supplier,
                Category = category,
                Brand = brand,
            };
            return View("~/Views/Admin/Product/Edit.cshtml", adminProductEditModel);
        }





        [Route("Admin/Product/Add")]
        public ActionResult Add()
        {
            
            AdminProductViewModel productIns = new AdminProductViewModel()
            {
                Supplier = Database.getContext().Supplier.ToList(),
                Category = Database.getContext().Category.ToList(),
                Brand = Database.getContext().Brand.ToList(),
            };

            return View("~/Views/Admin/Product/Add.cshtml", productIns);
        }

        [HttpPost]
        [Route("Admin/Product/Add/Post")]
        public ActionResult AddPost()
        {
            string title = Request["title"];
            string description = Request["description"];
            string sciname = Request["sci_name"];
            string expiryDate = Request["expiryDate"];
            int qty_stock = Convert.ToInt32(Request["qty_stock"]);
            int supplier = Convert.ToInt32(Request["supplier"]);
            int brand = Convert.ToInt32(Request["brand"]);
            int category = Convert.ToInt32(Request["category"]);

            HttpPostedFileBase fileAbsolute = Request.Files["fileAbsolute"];
            var filename = Path.GetFileName(fileAbsolute.FileName);
            var Imgpath = Path.Combine(Server.MapPath("~/Uploads"), filename);
            fileAbsolute.SaveAs(Imgpath);
            string imgUrl = "/Uploads/" + filename;

            Product addPRoduct = new Product()
            {
                Title = title,
                Description = description,
                Img = imgUrl,
                sci_name = sciname,
                qty_stock = qty_stock,
                SupplierId = supplier,
                BrandId = brand,
                CategoryId = category,
                ExpiryDate = expiryDate

            };

            Product insertedProduct = Database.getContext().Product.Add(addPRoduct);
            Database.getContext().SaveChanges();
            
            int noOf = Convert.ToInt32(Request["noOfPricing"]);

            for (int i=0;i<noOf;i++)
            {
                string _pricing_title = "pricing_title" + i;
                string _stock_price = "stock_price" + i;
                string _sale_price = "stock_price" + i;

                string pricing_title = Convert.ToString(Request[_pricing_title]);
                double stock_price = Convert.ToDouble(Request[_stock_price]);
                double sale_price = Convert.ToDouble(Request[_sale_price]);

                Pricing pricing = new Pricing() {
                    Title = pricing_title,
                    Product = insertedProduct,
                    stock_price = stock_price,
                    sale_price = sale_price
                };

                Database.getContext().Pricing.Add(pricing);
                Database.getContext().SaveChanges();

            }

            return RedirectToAction("ProductDashboard");
            //return Content(" ");

            /*if (prdt != null) {

                return View("~/Views/Admin/Product/Add.cshtml");
            }
            else
            {
                return Content("Error");
            }*/
            

           
        }

        [Route("admin/test")]
        public ActionResult test()
        {
            return View("~/Views/Admin/Product/pricing.cshtml");
        }

        [Route("admin/product/delete/{product_id}")]
        public ActionResult Delete(int product_id)
        {
            Product pro = Database.getContext().Product.SingleOrDefault(c => c.Id == product_id);

            Database.getContext().Product.Remove(pro);
            Database.getContext().SaveChanges();

            return RedirectToAction("ProductDashboard");
        }







    }
}