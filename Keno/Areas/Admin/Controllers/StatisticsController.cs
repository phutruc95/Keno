using Keno.Model;
using Keno.Repository;
using Keno.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Keno.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StatisticsController : Controller
    {
        private KenoContext db = new KenoContext();

        //
        // GET: /Admin/Statistics/
        public ActionResult Index(int productID, int? month)
        {
            ViewBag.Breadcrumb = new List<Breadcrumb>
            {
                new Breadcrumb("#", "Thống kê")
            };

            if (month == null) month = DateTime.Now.Month;
            List<Statistics> lstStatistics = db.Statistics.Where(s => s.ProductID == productID 
                && s.AccessDate.Month == month
                && s.AccessDate.Year == DateTime.Now.Year)
                .OrderBy(s => s.AccessDate).ToList();

            ViewBag.Months = new SelectList(db.Statistics.Select(m => m.AccessDate.Month).Distinct(), month);

            return View(lstStatistics);
        }
	}
}