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
        //[Bind(Include = "ID,ProductName,Image,Url,Price,SalePrice,IsOnSale,ProductTypeID")]
        public ActionResult Edit(Product product, HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {
                if (imgFile != null)
                {
                    if (!string.IsNullOrEmpty(product.Image))
                    {
                        string currentFilePath = Path.Combine(Server.MapPath("~/Content/UserImages/"), Path.GetFileName(product.Image));
                        if (System.IO.File.Exists(currentFilePath))
                        {
                            System.IO.File.Delete(currentFilePath);
                        }
                    }

                    string fileName = Path.GetFileNameWithoutExtension(imgFile.FileName);
                    string extension = Path.GetExtension(imgFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    product.Image = "~/Content/UserImages/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Content/UserImages/"), fileName);
                    imgFile.SaveAs(fileName);
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

            string currentFilePath = Path.Combine(Server.MapPath("~/Content/UserImages/"), Path.GetFileName(product.Image));
            if (System.IO.File.Exists(currentFilePath))
            {
                System.IO.File.Delete(currentFilePath);
            }

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
