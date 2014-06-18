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

        // <summary> 
        // PageSize: number of records to be displayed
        // PageNo: current page index
        // SortBy: column number
        // IsAsc: direction of sort 
        //</summary> 
        public ActionResult Index(SearchRequestModel request)
        {
           
            ProductViewModel productViewModel = FetchData(request);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Product", productViewModel);
            }

            return View(productViewModel);
        }

        public ProductViewModel FetchData(SearchRequestModel request)
        {
            if (request.CategoryId == null && request.SearchString == null && request.SortBy == 0)
            {
                request.IsAsc = true;
                request.SortBy = 1;
                request.PageNo = 1;
            }
            // the number of records(rows) that to be diplayed
            int defaultPageSize = 3;
            if (request.PageSize != null)
            {
                defaultPageSize = (int)request.PageSize;
            }
            int pageNumber = (request.PageNo ?? 1);
            var query = db.Categories.Select(c => new { c.Id, c.Name });
            //var products = db.Products.Where(s => ((request.CategoryId != null && s.CategoryId == request.CategoryId) && (!String.IsNullOrEmpty(request.SearchString) &&
            //  s.Name.Contains(request.SearchString))));
            var products = db.Products.Select(x => x);
            if (request.CategoryId != null || !String.IsNullOrEmpty(request.SearchString))
            {
                products = products.Where(s => s.CategoryId == request.CategoryId || s.Name.Contains(request.SearchString));
            }


            #region Sorting
            switch (request.SortBy)
            {
                case 1:
                    products = request.IsAsc ? products.OrderBy(p => p.Name) : products.OrderByDescending(p => p.Name);
                    break;

                case 2:
                    products = request.IsAsc ? products.OrderBy(p => p.Description) : products.OrderByDescending(p => p.Description);
                    break;

                default:
                    products = request.IsAsc ? products.OrderBy(p => p.Price) : products.OrderByDescending(p => p.Price);
                    break;
            }
            #endregion




            ProductViewModel productViewModel = new ProductViewModel()
            {
                ProductList = products.ToPagedList(pageNumber, defaultPageSize),
                Categories = new SelectList(query.AsEnumerable(), "Id", "Name"),
                TotalPrice = products.Select(x => x.Price).Sum(),
                TotalNoOfRec = products.Count(),
                SearchRequestModel = request,


            };
            return productViewModel;
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
        public ActionResult Delete(SearchRequestModel request)
        {

            try
            {
                Product product = db.Products.Find(request.Id);
                db.Products.Remove(product);
                db.SaveChanges();
                ProductViewModel productViewModel = FetchData(request);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Product", productViewModel);
                }

               
               // return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
            return null;
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
