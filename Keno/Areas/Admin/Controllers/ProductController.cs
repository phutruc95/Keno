using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Keno.Model;
using Keno.ViewModel;
using Keno.Repository;
using System.IO;
using System.Text;

namespace Keno.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private KenoContext db = new KenoContext();

        // GET: /Admin/Product/
        public ActionResult Index()
        {
            ViewBag.Breadcrumb = new List<Breadcrumb>
            {
                new Breadcrumb("#", "Sản phẩm")
            };

            var products = db.Products.Include(p => p.ProductType);
            return View(products.ToList());
        }

        // GET: /Admin/Product/Details/5
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
            return View(product);
        }

        // GET: /Admin/Product/Create
        public ActionResult Create()
        {
            ViewBag.Breadcrumb = new List<Breadcrumb>
            {
                new Breadcrumb(Url.Action("Index"), "Sản phẩm"),
                new Breadcrumb("#", "Thêm mới")
            };

            var product = new Product();
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ID", "TypeName");

            return View("Edit", product);
        }

        // POST: /Admin/Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,ProductName,Image,Url,Price,SalePrice,IsOnSale,ProductTypeID")] Product product, Stream stream)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Products.Add(product);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ID", "TypeName", product.ProductTypeID);
        //    return View(product);
        //}

        // GET: /Admin/Product/Edit/5
        [HttpGet]
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

            ViewBag.Breadcrumb = new List<Breadcrumb>
            {
                new Breadcrumb(Url.Action("Index"), "Sản phẩm"),
                new Breadcrumb("#", "Chỉnh sửa")
            };

            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ID", "TypeName", product.ProductTypeID);
            return View(product);
        }

        // POST: /Admin/Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProductName,Image,Url,Price,SalePrice,IsOnSale,ProductTypeID")]Product product, int[] buffer)
        {
            if (ModelState.IsValid)
            {
                //byte[] arrBuff = Encoding.ASCII.GetBytes(buffer);
                //byte[] arrBuff2 = System.IO.File.ReadAllBytes("D:\\rose0.jpeg");
                //System.IO.File.WriteAllBytes("D:\\rose.jpeg", buffer);

                byte[] result = new byte[buffer.Length];
                for (int i = 0; i < buffer.Length; i++)
                {
                    result[i] = (byte)buffer[i];
                }

                if (product.ID == 0)
                {
                    db.Products.Add(product);
                }
                else
                {
                    db.Entry(product).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ID", "TypeName", product.ProductTypeID);
            return View(product);
        }

        // GET: /Admin/Product/Delete/5
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

        // POST: /Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
