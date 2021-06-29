using DataObjects;
using LogicLayer;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        IUserManager discloneUserManager = new UserManager();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Manage()
        {
            try
            {
                var users = discloneUserManager.SelectAllUsers();
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                List<UserVM> usersWithIdentityAccounts = new List<UserVM>();
                foreach (var user in users)
                {
                    if(userManager.Users.Any(u => u.DiscloneUserID == user.UserID))
                    {
                        usersWithIdentityAccounts.Add(user);
                    }
                }
                return View(usersWithIdentityAccounts);
            }
            catch (Exception)
            {
                return new HttpNotFoundResult();
            }
            
        }
        public ActionResult ChangeIsAdmin(int id, bool isAdmin)
        {
            try
            {
                discloneUserManager.UpdateUserIsAdmin(id, !isAdmin);
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("Manage");
        }
    }
}