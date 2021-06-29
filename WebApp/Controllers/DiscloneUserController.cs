using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class DiscloneUserController : Controller
    {
        IUserManager discloneUserManager = new UserManager();
        // GET: DiscloneUser
        public ActionResult Manage(int id)
        {
            List<UserVM> users = null;
            try
            {
                Session["SelectedServerId"] = id;
                users = discloneUserManager.GetServerUserList(id);
            }
            catch (Exception)
            {

                throw;
            }
            
            return View(users);
        }
        [Authorize]
        public ActionResult RemoveFromServer(int id)
        {
            int serverId;
            try
            {
                serverId = (int)Session["SelectedServerId"];
                discloneUserManager.RemoveUserFromServerUserList(id, serverId);
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("Manage", "DiscloneUser", new { id=serverId });
        }
    }
}