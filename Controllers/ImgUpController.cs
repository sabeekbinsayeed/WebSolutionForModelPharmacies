using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSolutionForModelPharmacies.Controllers
{
    public class ImgUpController : Controller
    {
        // GET: ImgUp
        [HttpGet]
        [Route("img/")]
        public ActionResult Index()
        {
            return View();
        }
        // GET: ImgUp
        [HttpPost]
        [Route("img/")]
        public ActionResult upload()
        {
            HttpPostedFileBase fileAbsolute = Request.Files["fileAbsolute"];
            var filename = Path.GetFileName(fileAbsolute.FileName); //using System.IO;
            var path = Path.Combine(Server.MapPath("~/Uploads"), filename);
            fileAbsolute.SaveAs(path);
            //save this path into database
            return Content("done");
        }
    }
}