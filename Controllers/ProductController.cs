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
    public class ProductController : BaseController
    {
        // GET: Product
        [Route("product/{productId}/{urlTitle}")]
        public ActionResult Index(int productId,string urlTitle)
        {
            string title = urlTitle.Replace('_', ' ');
            Product product = Database.getContext().Product.Where(c => c.Id == productId).SingleOrDefault();

            List<Pricing> price = Database.getContext().Pricing.ToList();
            List<Pricing> nprice = price.Where(c => c.Product == product).ToList();
            //List<Pricing> price = Database.getContext().Pricing.Where(c => c.Product == product);

            //List<Product> RelatedProducts = Database.getContext().Product.Where(c => c.Title.StartsWith(product.Title) || c.Title.EndsWith(product.Title) || c.sci_name.StartsWith(product.sci_name) || c.sci_name.EndsWith(product.sci_name)).ToList();
            List<Product> RelatedProducts = Database.getContext().Product.Where(c => c.Title.StartsWith(product.sci_name)).ToList();
            //List<Product> RelatedProductsPass = Database.getContext().Product.Where(c => c.Title.StartsWith(product.sci_name)).ToList();
            if (RelatedProducts.Count<2)
            {
               RelatedProducts = Database.getContext().Product.ToList();
               RelatedProducts = Enumerable.Reverse(RelatedProducts).Take(4).Reverse().ToList();
               /*
                for (int i =0;i< RelatedProducts.Count;i++)
                {
                    var random = new Random();
                    int index = random.Next(RelatedProducts.Count);
                    RelatedProducts[i] = (RelatedProducts[index]);

                }
                RelatedProducts = RelatedProducts.Take(4).ToList();
                */
            };

            ProductViewModel productViewModel = new ProductViewModel()
            {
                Product = product,
                Price = nprice,
                RelatedProducts = RelatedProducts
            };
            //return Content(product.Title.ToString()+ product.Id.ToString());
            return View(productViewModel);
        }
    }
}