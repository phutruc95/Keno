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
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        //
        // GET: /Admin/User/
        public ActionResult Index()
        {
            ViewBag.Breadcrumb = new List<Breadcrumb>
            {
                new Breadcrumb("#", "Người dùng")
            };

            return View(db.Users.Where(u => u.UserName != "sa").OrderBy(u => u.UserName).ToList());
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
            string userRole = string.Empty;
            if (User.IsInRole("SuperAdmin")) userRole = "SuperAdmin";
            else if (User.IsInRole("Admin")) userRole = "Admin";

            var rolesList = db.Roles.Where(GetRolesCondition(userRole)).Select(r => new RoleViewModel() { Id = r.Id, Name = r.Name, IsGranted = false }).ToList();

            ViewBag.RolesList = rolesList;

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
            if (user == null || userManager.IsInRole(user.Id, "SuperAdmin"))
            {
                return HttpNotFound();
            }

            ViewBag.Breadcrumb = new List<Breadcrumb>
            {
                new Breadcrumb(Url.Action("Index"), "Tài khoản người dùng"),
                new Breadcrumb("#", "Chỉnh sửa")
            };

            string userRole = string.Empty;
            if (User.IsInRole("SuperAdmin")) userRole = "SuperAdmin";
            else if (User.IsInRole("Admin")) userRole = "Admin";

            var rolesList = db.Roles.Where(GetRolesCondition(userRole)).Select(r => new RoleViewModel() { Id = r.Id, Name = r.Name }).ToList();
            foreach (var role in rolesList)
            {
                if (userManager.IsInRole(user.Id, role.Name)) role.IsGranted = true;
            }
            ViewBag.RolesList = rolesList;

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUser user, string roleString, bool isResetPassword = false)
        {
            if (ModelState.IsValid)
            {
                string password = string.Empty;
                string[] roles = roleString.Split(',');

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

                var allRoles = db.Roles.ToList();
                
                //Remove current roles
                foreach (var role in allRoles)
                {
                    userManager.RemoveFromRole(user.Id, role.Name);
                }

                //Add selected roles
                foreach (string role in roles)
                {
                    userManager.AddToRole(user.Id, role);
                }

                db.SaveChanges();

                if (isResetPassword) return View("ReviewPassword", (object)($"Mật khẩu mặc định: <b>{password}</b>"));
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: /Admin/User/Delete/5
        public ActionResult Delete(string id)
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
                new Breadcrumb(Url.Action("Index"), "Tài khoản người dùng"),
                new Breadcrumb("#", "Xóa")
            };

            return View(user);
        }

        // POST: /Admin/User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser user = db.Users.Find(id);

            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private System.Linq.Expressions.Expression<Func<IdentityRole, bool>> GetRolesCondition(string role)
        {
            if (role == "Admin") return (r => r.Name != "SuperAdmin" && r.Name != "Admin");
            if (role == "SuperAdmin") return (r => r.Name != "SuperAdmin");
            return (r => false);
        }
    }
}
