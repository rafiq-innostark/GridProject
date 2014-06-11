using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MSIdentity.Models;
using PagedList;

namespace MSIdentity.Controllers
{
    public class ProductController : Controller
    {
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page, string categoryId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DescriptionSortParm = String.IsNullOrEmpty(sortOrder) ? "description_desc" : "";
            ViewBag.PriceSortParm = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";
            var query = db.Categories.Select(c => new { c.Id, c.Name });
            ViewBag.Categories = new SelectList(query.AsEnumerable(), "Id", "Name");
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var products = from s in db.Products
                           select s;
            //if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(categoryId))
            //{
            //    products = products.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper())
            //                           && s.CategoryId == categoryId);
            //}  
            if ( !String.IsNullOrEmpty(categoryId))
            {
                products = products.Where(s=> s.CategoryId == categoryId);
            }
            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(s => s.Name);
                    break;
                case "description_desc":
                    products = products.OrderByDescending(s => s.Description);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(s => s.Price);
                    break;
                default:  // Name ascending 
                    products = products.OrderBy(s => s.Name);
                    break;
            }
            ViewBag.TotalNoOfRec = products.Count();
            //products = products.OrderByDescending(s => s.Name);
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Product/
        //public ActionResult Index(FormCollection form, int? page, string sort, string sortdir)
        //{
        //    //ApplicationDbContext obj = new ApplicationDbContext();
        //    //var categoryId = form["Id"];
        //    //var searchData = form["Search_Data"];
        //    //var a = form["X-Requested-With"];
        //    //var query = obj.Categories.Select(c => new { c.Id, c.Name });
        //    //ViewBag.Categories = new SelectList(query.AsEnumerable(), "Id", "Name");
        //    //IEnumerable<Product> model = null;
        //    //if (sort == "Name")
        //    //{
        //    //    model = obj.Products.Where(x => x.CategoryId == categoryId && x.Name.Contains(searchData)).OrderBy(x => x.Name);
        //    //    return PartialView(model);
        //    //}
        //    //else if (sort == "Description")
        //    //{
        //    //    model = obj.Products.Where(x => x.CategoryId == categoryId && x.Name.Contains(searchData)).OrderBy(x => x.Description);
        //    //    return PartialView(model);
        //    //}
        //    //else if (sort == "Price")
        //    //{
        //    //    model = obj.Products.Where(x => x.CategoryId == categoryId && x.Name.Contains(searchData)).OrderBy(x => x.Price);
        //    //    return PartialView(model);

        //    //}

        //    //else
        //    //{

        //    //    model =
        //    //        obj.Products.Where(x => x.CategoryId == categoryId && x.Name.Contains(searchData))
        //    //            .OrderBy(x => x.Name);
        //    //    return PartialView(model);

        //    //}

        //    return View();

        //}

        ////
        // GET: /Product/Details/5
        public ActionResult Details()
        {

            return View();
        }

        //
        // GET: /Product/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Product/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
