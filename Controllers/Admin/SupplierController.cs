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
    public class SupplierController : AdminBaseController
    {

        [HttpGet]
        [Route("admin/supplier/dashboard")]
        public ActionResult Index()
        {
            AdminDashboardViewModel advm = new AdminDashboardViewModel()
            {
                Supplier = Database.getContext().Supplier.ToList(),

            };

            return View("~/Views/Admin/Supplier/Index.cshtml", advm);
        }

        [HttpGet]
        [Route("admin/supplier/add")]
        public ActionResult Add()
        {

            return View("~/Views/Admin/Supplier/Add.cshtml");
        }

        [HttpPost]
        [Route("admin/supplier/add/post")]
        public ActionResult AddSupplierPost()
        {
            Supplier cat = new Supplier
            {
                Title = Request["title"],
            };
            Database.getContext().Supplier.Add(cat);
            Database.getContext().SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("admin/supplier/delete/{id}")]
        public ActionResult Delete(int Id)
        {

            Supplier cat = Database.getContext().Supplier.First(c => c.Id == Id);

            Database.getContext().Supplier.Remove(cat);
            Database.getContext().SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("admin/supplier/edit/{catId}/{catTitle}")]
        public ActionResult Edit(int catId, string catTitle)
        {

            Supplier cat = Database.getContext().Supplier.SingleOrDefault(c => c.Id == catId);
            cat.Title = catTitle;
            Database.getContext().SaveChanges();

            return RedirectToAction("Index");
        }

    }
}