﻿using Keno.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
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
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Sales()
        {
            return View();
        }
    }
}