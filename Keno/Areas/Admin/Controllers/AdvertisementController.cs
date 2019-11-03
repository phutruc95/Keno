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
using Keno.ViewModel;
using System.IO;

namespace Keno.Areas.Admin.Controllers
{
    public class AdvertisementController : Controller
    {
        private KenoContext db = new KenoContext();

        // GET: /Admin/Advertisement/
        public ActionResult Index()
        {
            ViewBag.Breadcrumb = new List<Breadcrumb>
            {
                new Breadcrumb("#", "Khuyến mãi")
            };

            var advertisements = db.Advertisements.Include(a => a.Product);
            return View(advertisements.ToList());
        }

        // GET: /Admin/Advertisement/Create
        public ActionResult Create()
        {
            ViewBag.Breadcrumb = new List<Breadcrumb>
            {
                new Breadcrumb(Url.Action("Index"), "Khuyến mãi"),
                new Breadcrumb("#", "Thêm mới")
            };

            Advertisement advertisement = new Advertisement();
            ViewBag.ProductID = new SelectList(db.Products, "ID", "ProductName");
            return View("Edit", advertisement);
        }

        // POST: /Admin/Advertisement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        // GET: /Admin/Advertisement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = db.Advertisements.Find(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }

            ViewBag.Breadcrumb = new List<Breadcrumb>
            {
                new Breadcrumb(Url.Action("Index"), "Khuyến mãi"),
                new Breadcrumb("#", "Chỉnh sửa")
            };

            ViewBag.ProductID = new SelectList(db.Products, "ID", "ProductName", advertisement.ProductID);
            return View(advertisement);
        }

        // POST: /Admin/Advertisement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Content,Image,Url,ProductID")] Advertisement advertisement, HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {
                if (imgFile != null)
                {
                    if (!string.IsNullOrEmpty(advertisement.Image))
                    {
                        string currentFilePath = Path.Combine(Server.MapPath("~/Content/UserImages/"), Path.GetFileName(advertisement.Image));
                        if (System.IO.File.Exists(currentFilePath))
                        {
                            System.IO.File.Delete(currentFilePath);
                        }
                    }

                    string fileName = Path.GetFileNameWithoutExtension(imgFile.FileName);
                    string extension = Path.GetExtension(imgFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    advertisement.Image = "~/Content/UserImages/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Content/UserImages/"), fileName);
                    imgFile.SaveAs(fileName);
                }
                if (advertisement.ID == 0)
                {
                    db.Advertisements.Add(advertisement);
                }
                else
                {
                    db.Entry(advertisement).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Products, "ID", "ProductName", advertisement.ProductID);
            return View(advertisement);
        }

        // GET: /Admin/Advertisement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = db.Advertisements.Find(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            return View(advertisement);
        }

        // POST: /Admin/Advertisement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Advertisement advertisement = db.Advertisements.Find(id);

            if (!string.IsNullOrEmpty(advertisement.Image))
            {
                string currentFilePath = Path.Combine(Server.MapPath("~/Content/UserImages/"), Path.GetFileName(advertisement.Image));
                if (System.IO.File.Exists(currentFilePath))
                {
                    System.IO.File.Delete(currentFilePath);
                }
            }

            db.Advertisements.Remove(advertisement);
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
