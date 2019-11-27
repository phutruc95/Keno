using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Keno.ViewModel;
using Keno.Repository;
using System.Data.Entity;

namespace Keno.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class ThemeController : Controller
    {
        private KenoContext db = new KenoContext();

        // GET: Admin/Theme
        public ActionResult Index()
        {
            ViewBag.Breadcrumb = new List<Breadcrumb>
            {
                new Breadcrumb("#", "Chủ đề")
            };
            return View();
        }

        [HttpPost]
        public JsonResult SetTheme(string theme)
        {
            try
            {
                var themeSetting = db.AppSettings.Find("Theme");
                themeSetting.Value = theme;
                db.Entry(themeSetting).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { IsSuccessful = 1 });
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccessful = 0, Msg = ex.Message });
            }
        }
    }
}