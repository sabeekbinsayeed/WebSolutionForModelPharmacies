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
    public class BrandController : AdminBaseController
    {
        // GET: Brand
        [Route("admin/brand/dashboard")]
        public ActionResult Index()
        {
            AdminDashboardViewModel advm = new AdminDashboardViewModel() {
                Brand = Database.getContext().Brand.ToList()
            }; 

            return View("~/Views/Admin/Brand/Index.cshtml", advm);
        }


        [HttpPost]
        [Route("admin/brand/add/post")]
        public ActionResult AddBrandPost()
        {
            Brand cat = new Brand
            {
                title = Request["title"],
            };
            Database.getContext().Brand.Add(cat);
            Database.getContext().SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("admin/brand/delete/{id}")]
        public ActionResult Delete(int Id)
        {

            Brand cat = Database.getContext().Brand.First(c => c.Id == Id);

            Database.getContext().Brand.Remove(cat);
            Database.getContext().SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("admin/brand/edit/{catId}/{catTitle}")]
        public ActionResult Edit(int catId, string catTitle)
        {
            Brand cat = Database.getContext().Brand.SingleOrDefault(c => c.Id == catId);
            cat.title = catTitle;
            Database.getContext().SaveChanges();

            return RedirectToAction("Index");
        }



    }
}