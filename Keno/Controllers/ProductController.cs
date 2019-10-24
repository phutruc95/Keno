using Keno.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Keno.Controllers
{
    public class ProductController : Controller
    {
        private KenoContext db = new KenoContext();

        // GET: Product
        public ActionResult Index(int typeID = 0)
        {
            ViewBag.ListProductType = db.ProductTypes.ToList();
            ViewBag.TypeID = typeID;

            var products = db.Products.Where(p => typeID == 0 || p.ProductTypeID == typeID).Include(p => p.ProductType);
            return View(products.ToList());
        }
    }
}