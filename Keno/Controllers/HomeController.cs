using Keno.Model;
using Keno.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Keno.Controllers
{
    public class HomeController : Controller
    {
        private KenoContext db = new KenoContext();

        public ActionResult Index()
        {
            ViewBag.ListProductType = db.ProductTypes.ToList();
            var advertisements = db.Advertisements.Take(8).ToList();
            return View(advertisements);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult Sales()
        {
            var userProperty = db.UserProperties.Find(User.Identity.Name);
            if (userProperty == null)
            {
                userProperty = new Model.UserProperty();
                userProperty.Username = User.Identity.Name;
                userProperty.AttendanceList = "0000000";
                db.UserProperties.Add(userProperty);
                db.SaveChanges();
            }
            var offers = db.Offers.ToList();
            ViewBag.UserProperty = userProperty;
            return View(offers);
        }

        [HttpPost]
        [Authorize]
        public JsonResult ChargeSaleCode(int offerID)
        {
            try
            {
                var offer = db.Offers.Find(offerID);
                var userProperty = db.UserProperties.Find(User.Identity.Name);

                if (offer == null || userProperty == null) return Json(new { isSuccess = false, msg = "Tài khoản không tồn tại" });
                if (userProperty.Coins < offer.CoinsConsumed) return Json(new { isSuccess = false, msg = "Số xu không đủ" });

                var saleCode = new SaleCode()
                {
                    Username = User.Identity.Name,
                    Percent = offer.Percent,
                    Code = Utility.CommonFunction.RandomString(10),
                    OverduedDate = DateTime.Now
                };

                db.SaleCodes.Add(saleCode);
                userProperty.Coins = userProperty.Coins - offer.CoinsConsumed;
                db.Entry(userProperty).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new { isSuccess = true, msg = "", leftcoins = userProperty.Coins });
            }
            catch (Exception ex)
            {
                return Json(new { isSuccess = false, msg = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult CheckAttendance()
        {
            var userProperty = db.UserProperties.Find(User.Identity.Name);
            if (userProperty != null)
            {
                userProperty.Coins += 50;
                if (userProperty.AttendanceList == null) userProperty.AttendanceList = "0000000";
                int dayOfWeek = (int)DateTime.Now.DayOfWeek;
                StringBuilder strBuilder = new StringBuilder(userProperty.AttendanceList);
                strBuilder[dayOfWeek] = '1';
                userProperty.AttendanceList = strBuilder.ToString();

                db.Entry(userProperty).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new { leftcoins = userProperty.Coins });
        }
    }
}