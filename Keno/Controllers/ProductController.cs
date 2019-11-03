using Keno.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Net;
using Keno.Model;

namespace Keno.Controllers
{
    public class ProductController : Controller
    {
        private KenoContext db = new KenoContext();

        // GET: Product
        public ActionResult Index(int typeID = 0, int page = 1, string searchStr = "")
        {
            searchStr = searchStr.ToLower();
            ViewBag.ListProductType = db.ProductTypes.ToList();
            ViewBag.TypeID = typeID;

            var products = db.Products.Where(p => (typeID == 0 || p.ProductTypeID == typeID) && (p.ProductName.ToLower().Contains(searchStr) || p.ShortDesc.ToLower().Contains(searchStr)))
                .Include(p => p.ProductType).OrderByDescending(p => p.ID);

            int pageSize = 9;
            int pageNumber = page;
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Product/Details/5
        public ActionResult Details(int? id)
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

            ViewBag.ListProductType = db.ProductTypes.ToList();

            return View(product);
        }
    }
}