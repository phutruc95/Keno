using Keno.Models;
using Keno.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Keno.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //
        // GET: /Admin/User/
        public ActionResult Index()
        {
            ViewBag.Breadcrumb = new List<Breadcrumb>
            {
                new Breadcrumb("#", "Người dùng")
            };

            return View(db.Users.ToList());
        }

        // GET: /Admin/User/Create
        public ActionResult Create()
        {
            ViewBag.Breadcrumb = new List<Breadcrumb>
            {
                new Breadcrumb(Url.Action("Index"), "Người dùng"),
                new Breadcrumb("#", "Thêm mới")
            };

            var user = new ApplicationUser();
            ViewBag.ProductTypeID = new SelectList(db.Roles, "Id", "Name");

            return View("Edit", user);
        }

        //  POST: /Admin/User/Edit
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            ViewBag.Breadcrumb = new List<Breadcrumb>
            {
                new Breadcrumb(Url.Action("Index"), "Người dùng"),
                new Breadcrumb("#", "Chỉnh sửa")
            };

            ViewBag.ProductTypeID = new SelectList(db.Roles, "Id", "Name");

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUser user, bool isResetPassword = false)
        {
            if (ModelState.IsValid)
            {
                string password = string.Empty;
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                if (string.IsNullOrEmpty(user.PasswordHash))
                {
                    isResetPassword = true;

                    password = Utility.CommonFunction.RandomString(10);
                    userManager.Create(user, password);
                }
                else
                {
                    if (isResetPassword)
                    {
                        password = Utility.CommonFunction.RandomString(10);
                        user.PasswordHash = (new PasswordHasher()).HashPassword(password);
                    }

                    db.Entry(user).State = EntityState.Modified;
                }

                db.SaveChanges();

                if (isResetPassword) return View("ReviewPassword", (object)password);
                return RedirectToAction("Index");
            }

            return View(user);
        }
    }
}
