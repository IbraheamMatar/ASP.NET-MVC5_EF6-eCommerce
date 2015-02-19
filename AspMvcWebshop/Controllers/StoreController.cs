using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspMvcWebshop.Models;

namespace AspMvcWebshop.Controllers
{
    public class StoreController : Controller
    {
        StoreEntities storeDB = new StoreEntities();

        //
        // GET: /Store/
        public ActionResult Index()
        {
            var categories = storeDB.Categories.ToList();        
            return View(categories);
        }
        //
        // GET: /Store/Browse
        //returns NULL?????
        public ActionResult Browse(string category)
        {
            //prevent Javascript - HtmlEncode
            var categoryModel = storeDB.Categories.Include("Products")
                .SingleOrDefault(c => c.Name.Contains(category));
                //lambda expression, we want a single object that it's
                //name matches the defined value.                
            
            return View(categoryModel);
        }

        //
        // GET: /Store/Details/5
        public ActionResult Details(int id)
        {
            var product = storeDB.Products.Find(id);

            return View(product);
        }

        //
        // GET: /Store/GenreMenu
        [ChildActionOnly]
        public ActionResult CategoryMenu()
        {
            var genres = storeDB.Categories.ToList();
            return PartialView(genres);
        }

        //partial views logic
        [ChildActionOnly]
        public ActionResult AdminMenu()
        {
            if (User.IsInRole("Administrator")) // TODO: Remove magic string
            {
                return PartialView("AdminMenu");
            }
            return new EmptyResult();
        }

        [ChildActionOnly]
        public ActionResult GuestMenu()
        {
            if (!User.Identity.IsAuthenticated) // TODO: Remove magic string
            {
                return PartialView("GuestMenu");
            }
            return new EmptyResult();
        }

        [ChildActionOnly]
        public ActionResult RegisteredUser()
        {
            if (User.Identity.IsAuthenticated && !User.IsInRole("Administrator")) // TODO: Remove magic string
            {
                return PartialView("RegisteredUserMenu");
            }
            return new EmptyResult();
        }  
    }
}
