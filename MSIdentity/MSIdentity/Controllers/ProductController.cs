using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MSIdentity.MainRepositories;
using MSIdentity.Models;
using PagedList;

namespace MSIdentity.Controllers
{

    public class ProductController : Controller
    {
        private static IUnityContainer container;

        public ProductController()
        {
            container = MvcApplication.Container;

        }

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
            int defaultPageSize = 5;
            if (request.PageSize != null)
            {
                defaultPageSize = (int)request.PageSize;
            }
            int pageNumber = (request.PageNo ?? 1);
            var productRepo = container.Resolve<IProductRepository>();
            var products = productRepo.GetAllProducts(request);
            var categoryRepo = container.Resolve<ICategoryRepository>();
            var categries = categoryRepo.GetAllCategories();
            var productViewModel = new ProductViewModel()
                        {
                            ProductList = products.ToPagedList(pageNumber, defaultPageSize),
                            Categories = new SelectList(categries.AsEnumerable(), "Id", "Name"),
                            TotalPrice = products.Select(x => x.Price).Sum(),
                            TotalNoOfRec = products.Count(),
                            SearchRequestModel = request,


                        };
            return productViewModel;
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
                    var productRepo = container.Resolve<IProductRepository>();
                    productRepo.Add(product);
                    productRepo.SaveChanges();
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
            var categoryRepo = container.Resolve<ICategoryRepository>();
            var categries = categoryRepo.GetAllCategories();
            ViewBag.Categories = new SelectList(categries.AsEnumerable(), "Id", "Name");
        }
        //
        // GET: /Product/Edit/5
        public ActionResult Edit(int id)
        {

            var productRepo = container.Resolve<IProductRepository>();
            Product product = productRepo.Find(id);
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
                    var productRepo = container.Resolve<IProductRepository>();
                    productRepo.Update(product);
                    productRepo.SaveChanges();
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
            var productRepo = container.Resolve<IProductRepository>();
            var product = productRepo.Find(request.Id);
            try
            {
                productRepo.Delete(product);
                productRepo.SaveChanges();
                ProductViewModel productViewModel = FetchData(request);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Product", productViewModel);
                }

            }
            catch
            {
                return View();
            }
            return null;
        }


    }
}
