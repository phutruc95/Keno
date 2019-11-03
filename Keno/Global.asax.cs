using Keno.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Keno
{
    public class MvcApplication : System.Web.HttpApplication
    {
        KenoContext db = new KenoContext();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var record = db.Statistics.Where(r => r.AccessDate.Day == DateTime.Now.Day 
                                            && r.AccessDate.Month == DateTime.Now.Month
                                            && r.AccessDate.Year == DateTime.Now.Year).FirstOrDefault();
            if (record == null)
            {
                db.Statistics.Add(new Model.Statistics { ProductID = 0, Accessions = 1, AccessDate = DateTime.Now });
            }
            else
            {
                record.Accessions += 1;
                db.Entry(record).State = EntityState.Modified;
            }

            db.SaveChanges();
        }
    }
}
