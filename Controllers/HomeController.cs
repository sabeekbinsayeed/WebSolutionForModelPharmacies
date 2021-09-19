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

    interface IHomeController
    {
        ActionResult Index();
        ActionResult HomeCategory(int cat_id);
        ActionResult HomeBrand(int brand_id);
        ActionResult HomePage(int page_id);
        ActionResult Contact();
        ActionResult PrescriptionUpload();
        ActionResult PrescriptionUploadpost();

    }

    public class HomeController : BaseController , IHomeController
    {
          
        [Route("")]
        public ActionResult Index()
        {   
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                //Category = Database.getContext().Category.ToList(),
                Product = Database.getContext().Product.ToList(),
                //Customer = _context.Customer.SingleOrDefault(c => c.Name == "Kazi"),
                Price = Database.getContext().Pricing.ToList(),
            };

            //return Content(homeViewModel.Customer.Name);
            return View(homeViewModel);
        }
                        
        [Route("category/{cat_id}")]
        public ActionResult HomeCategory(int cat_id)
        {   
            
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                //Category = Database.getContext().Category.ToList(),
                Product = Database.getContext().Product.Where(c => c.CategoryId == cat_id).ToList(),
                //Customer = _context.Customer.SingleOrDefault(c => c.Name == "Kazi"),
                Price = Database.getContext().Pricing.ToList(),
            };

            //return Content(homeViewModel.Customer.Name);
            return View("~/Views/Home/index.cshtml", homeViewModel);
        }                        
        [Route("brand/{brand_id}")]
        public ActionResult HomeBrand(int brand_id)
        {   
            
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                //Category = Database.getContext().Category.ToList(),
                Product = Database.getContext().Product.Where(c => c.BrandId == brand_id).ToList(),
                //Customer = _context.Customer.SingleOrDefault(c => c.Name == "Kazi"),
                Price = Database.getContext().Pricing.ToList(),
            };

            //return Content(homeViewModel.Customer.Name);
            return View("~/Views/Home/index.cshtml", homeViewModel);
        }        
        
        [Route("page/{page_id}")]
        public ActionResult HomePage(int page_id)
        {   
            
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                //Category = Database.getContext().Category.ToList(),
                Product = Database.getContext().Product.ToList(),
                //Customer = _context.Customer.SingleOrDefault(c => c.Name == "Kazi"),
                Price = Database.getContext().Pricing.ToList(),
            };

            //return Content(homeViewModel.Customer.Name);
            return View("~/Views/Home/index.cshtml", homeViewModel);
        }
                     
        
        [Route("contact")]
        public ActionResult Contact()
        {   
            
            
            return View("~/Views/Home/contact.cshtml");
        }
                  
        /* Upload Process */
        [Route("home/prescription/upload/process")]
        public ActionResult PrescriptionUpload()
        {
            

            string prescription_path = dUpload.Upload("upload_prescription", "~/Uploads/");

            //Response.Write("<script> alert('"+ path + "') </script>");

            HomeViewModel homeViewModel = new HomeViewModel()
            {
                //Category = Database.getContext().Category.ToList(),
                Product = Database.getContext().Product.ToList(),
                //Customer = _context.Customer.SingleOrDefault(c => c.Name == "Kazi"),
                Price = Database.getContext().Pricing.ToList(),

                PrescriptionPath = prescription_path,

            };

            //return Content(homeViewModel.Customer.Name);
            return View("~/Views/Home/PrescriptionProcess.cshtml",homeViewModel);
        }


        [Route("home/prescription/handle")]
        public ActionResult PrescriptionUploadpost()
        {

            string getImageProcessedData = helper.dGetRequest.GetUrlParameter(Request, "key");

            string[] split = getImageProcessedData.Split('-');            //return Content(getImageProcessedData);

            List<Product> products = Database.getContext().Product.ToList();
            List<Product> productFound = new List<Product>();
         


            foreach (string sss in split)
            {
                //Response.Write(sss);
                if (Database.getContext().Product.FirstOrDefault(c => c.Title.StartsWith(sss)) != null)
                {
                    Product pdts = Database.getContext().Product.FirstOrDefault(c => c.Title.StartsWith(sss));
                    //Response.Write(pdts.Title);
                    productFound.Add(pdts);
                }
            }


            List<Product> prescriptionProduct = productFound.Distinct().ToList();


            HomeViewModel homeViewModel = new HomeViewModel()
            {
                //Category = Database.getContext().Category.ToList(),
                Product = prescriptionProduct,
                //Customer = _context.Customer.SingleOrDefault(c => c.Name == "Kazi"),
                Price = Database.getContext().Pricing.ToList(),
            };

            //return Content(homeViewModel.Customer.Name);
            return View("~/Views/Home/Index.cshtml", homeViewModel);




        }


























        [HttpPost]
        [Route("search")]
        public JsonResult Search(string inputText)
        {
            List<Product> products = new List<Product>();
            products = Database.getContext().Product.Where(c => c.Title.StartsWith(inputText) ).ToList();
            //List<Product> products = Database.getContext().Product.ToList();
            /*List<Product> products = new List<Product>();
            if (inputText == null)
            {
                products = Database.getContext().Product.ToList();
            }
            else
            {
                products = Database.getContext().Product.Where(c => c.Title.StartsWith(inputText)).ToList();
            }*/
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

       
    }
}