using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspMvcWebshop.Models;

namespace AspMvcWebshop.Controllers
{
    public class HomeController : Controller
    {

        StoreEntities storeDB = new StoreEntities();
        //
        // GET: /Home/
        public ActionResult Index()
        {
            // Get most popular items
            var products = GetTopSellingProducts(4);

            return View(products);

            //return "Hello from home";
        }

        //queries the DB for top selling products
        private List<Product> GetTopSellingProducts(int count)
        {
            // Group the order details by album and return
            // the albums with the highest count
            return storeDB.Products
                .OrderByDescending(a => a.OrderDetails.Count())
                .Take(count)
                .ToList();
        }
    }
}
