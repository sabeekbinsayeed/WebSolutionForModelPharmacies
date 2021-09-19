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
    public class CategoryController : Controller
    {



        [HttpGet]
        [Route("admin/category/dashboard")]    
        public ActionResult Index()
        {
            AdminDashboardViewModel advm = new AdminDashboardViewModel()
            {
                Category = Database.getContext().Category.ToList(),

            };

            return View("~/Views/Admin/Category/Index.cshtml", advm);
        }

        [HttpGet]
        [Route("admin/category/add")]    
        public ActionResult Add()
        {
            return View("~/Views/Admin/Category/Add.cshtml");
        }

        [HttpPost]
        [Route("admin/category/add/post")]
        public ActionResult AddCategoryPost()
        {
            Category cat = new Category
            {
                Title = Request["title"],
            };
            Database.getContext().Category.Add(cat);
            Database.getContext().SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("admin/category/delete/{id}")]
        public ActionResult Delete(int Id)
        {

            Category cat = Database.getContext().Category.First(c => c.Id == Id);

            Database.getContext().Category.Remove(cat);
            Database.getContext().SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("admin/category/edit/{catId}/{catTitle}")]
        public ActionResult Edit(int catId,string catTitle)
        {

            Category cat = Database.getContext().Category.SingleOrDefault(c => c.Id == catId);
            cat.Title = catTitle;
            Database.getContext().SaveChanges();

            return RedirectToAction("Index");
        }



    }
}