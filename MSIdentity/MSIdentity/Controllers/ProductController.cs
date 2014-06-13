using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MSIdentity.Models;
using PagedList;

namespace MSIdentity.Controllers
{
    public class ProductController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(int? pageSize, string currentFilter, string searchString, int? page, int? categoryId, int sortBy = 1, bool isAsc = true)
        {

            //ViewBag.CurrentSort = sortOrder;
            //ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewBag.DescriptionSortParm = String.IsNullOrEmpty(sortOrder) ? "description_desc" : "";
            //ViewBag.PriceSortParm = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";

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
            if (categoryId != null)
            {
                products = products.Where(s => s.CategoryId == categoryId);
            }
            #region Sorting
            switch (sortBy)
            {
                case 1:
                    products = isAsc ? products.OrderBy(p => p.Name) : products.OrderByDescending(p => p.Name);
                    break;

                case 2:
                    products = isAsc ? products.OrderBy(p => p.Description) : products.OrderByDescending(p => p.Description);
                    break;

                default:
                    products = isAsc ? products.OrderBy(p => p.Price) : products.OrderByDescending(p => p.Price);
                    break;
            }
            #endregion
            ViewBag.SortBy = sortBy;
            ViewBag.IsAsc = isAsc;

            ViewBag.TotalNoOfRec = products.Count();
           
            int defaultPageSize = 3;
            if (pageSize != null)
            {
                defaultPageSize = (int)pageSize;
            }
            ViewBag.pageSize = defaultPageSize;
            ViewBag.totalPrice = products.Select(x => x.Price).Sum();
            int pageNumber = (page ?? 1);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Product", products.ToPagedList(pageNumber, defaultPageSize));
            }
            return View(products.ToPagedList(pageNumber, defaultPageSize));
        }

        public ActionResult Details()
        {

            return View();
        }

          [HttpGet]
        public ActionResult Create()
        {
            PopulateCategoryDropDownList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        // GET: /Product/Create
        public ActionResult Create([Bind(Include = "CategoryId,Name, Description, Price")]Product product)
        {
           

            try
            {
                if (ModelState.IsValid)
                {
                   
                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
          
            return View(product);
        }
        private void PopulateCategoryDropDownList()
        {
            var query = db.Categories.Select(c => new { c.Id, c.Name });
            ViewBag.Categories = new SelectList(query.AsEnumerable(), "Id", "Name");
        } 
            //
        // GET: /Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            PopulateCategoryDropDownList();
            return View(product);
        }

        //
        // POST: /Product/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,CategoryId,Name, Description, Price")]Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return View(product);
        }

        //
        // GET: /Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
           
        }

        //
        // POST: /Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Product product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
