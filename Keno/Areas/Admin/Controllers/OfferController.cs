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
    [Authorize(Roles = "Admin, SuperAdmin, Vendor")]
    public class OfferController : Controller
    {
        private KenoContext db = new KenoContext();

        // GET: /Admin/Offer/
        public ActionResult Index()
        {
            ViewBag.Breadcrumb = new List<Breadcrumb>
            {
                new Breadcrumb("#", "Mã giảm giá")
            };

            return View(db.Offers.ToList());
        }


        // GET: /Admin/Offer/Create
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult Create()
        {
            ViewBag.Breadcrumb = new List<Breadcrumb>
            {
                new Breadcrumb(Url.Action("Index"), "Mã giảm giá"),
                new Breadcrumb("#", "Thêm mới")
            };

            return View();
        }

        // POST: /Admin/Offer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Percent,OverduedDate,Desc,Image,CoinsConsumed")] Offer offer, HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(imgFile.FileName);
                string extension = Path.GetExtension(imgFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                offer.Image = "~/Content/UserImages/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Content/UserImages/"), fileName);
                imgFile.SaveAs(fileName);

                db.Offers.Add(offer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(offer);
        }

        // GET: /Admin/Offer/Delete/5
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }

            ViewBag.Breadcrumb = new List<Breadcrumb>
            {
                new Breadcrumb(Url.Action("Index"), "Mã giảm giá"),
                new Breadcrumb("#", "Xóa")
            };

            return View(offer);
        }

        // POST: /Admin/Offer/Delete/5
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Offer offer = db.Offers.Find(id);

            if (!string.IsNullOrEmpty(offer.Image))
            {
                string currentFilePath = Path.Combine(Server.MapPath("~/Content/UserImages/"), Path.GetFileName(offer.Image));
                if (System.IO.File.Exists(currentFilePath))
                {
                    System.IO.File.Delete(currentFilePath);
                }
            }

            db.Offers.Remove(offer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /Admin/Offer/OfferApplication
        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin, Vendor")]
        public ActionResult OfferApplication()
        {
            ViewBag.Breadcrumb = new List<Breadcrumb>
            {
                new Breadcrumb(Url.Action("Index"), "Mã giảm giá"),
                new Breadcrumb("#", "Áp dụng")
            };

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin, Vendor")]
        public JsonResult CheckOfferCode(string code)
        {
            try
            {
                SaleCode saleCode = db.SaleCodes.Find(code);
                if (saleCode != null)
                {
                    return Json(new { IsSuccessful = 1, Percent = saleCode.Percent, Msg = string.Empty });
                }
                else
                {
                    return Json(new { IsSuccessful = 0, Msg = "Mã giảm giá không đúng hoặc đã được sử dụng!"});
                }
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccessful = 0, Msg = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin, Vendor")]
        public JsonResult ApplySaleCode(string code)
        {
            try
            {
                SaleCode saleCode = db.SaleCodes.Find(code);

                if (saleCode != null)
                {
                    db.SaleCodes.Remove(saleCode);
                    db.SaveChanges();
                    return Json(new { IsSuccessful = 1 });
                }

                else return Json(new { IsSuccessful = 0 });
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccessful = 0 });
            }
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
