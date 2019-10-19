using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Keno.Model;
using Keno.Repository;

namespace Keno.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductTypeController : Controller
    {
        private KenoContext db = new KenoContext();

        // GET: /Admin/ProductType/
        public ActionResult Index()
        {
            return View(db.ProductTypes.ToList());
        }

        // GET: /Admin/ProductType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductType producttype = db.ProductTypes.Find(id);
            if (producttype == null)
            {
                return HttpNotFound();
            }
            return View(producttype);
        }

        // GET: /Admin/ProductType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/ProductType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,TypeName")] ProductType producttype)
        {
            if (ModelState.IsValid)
            {
                db.ProductTypes.Add(producttype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(producttype);
        }

        // GET: /Admin/ProductType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductType producttype = db.ProductTypes.Find(id);
            if (producttype == null)
            {
                return HttpNotFound();
            }
            return View(producttype);
        }

        // POST: /Admin/ProductType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,TypeName")] ProductType producttype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producttype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producttype);
        }

        // GET: /Admin/ProductType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductType producttype = db.ProductTypes.Find(id);
            if (producttype == null)
            {
                return HttpNotFound();
            }
            return View(producttype);
        }

        // POST: /Admin/ProductType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductType producttype = db.ProductTypes.Find(id);
            db.ProductTypes.Remove(producttype);
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
