using DataObjects;
using LogicLayer;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Configuration;

namespace WebApp.Controllers
{
    public class ServerController : Controller
    {
        IServerManager discloneServerManager = new ServerManager();

        public ActionResult Details(int? id)
        {
            if(id != null)
            {
                Session["SelectedServerId"] = id;
            }           
            return RedirectToAction("Index", "Disclone");
        }
        [ChildActionOnly]
        public PartialViewResult List()
        {
            var id = this.User.Identity.GetUserId();
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.First(u => u.Id == id);

            if (user == null)
            {
                HttpNotFound();
            }
            List<ServerVM> servers = null;
            try
            {
                Session["DiscloneUserId"] = user.DiscloneUserID;
                Session["DiscloneUserName"] = user.DiscloneUserName;
                servers = discloneServerManager.GetUserServerList(user.DiscloneUserID);
            }
            catch (Exception)
            {

            }
            return PartialView(servers);
        }
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Create(ServerVM server)
        {
            if (ModelState.IsValid)
            {
                if(Session["DiscloneUserId"] != null)
                {
                    server.OwnerUserID = (int)Session["DiscloneUserId"];
                }
                else
                {
                    var id = this.User.Identity.GetUserId();
                    var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    var user = userManager.Users.First(u => u.Id == id);

                    server.OwnerUserID = user.DiscloneUserID;
                }
                
                discloneServerManager.AddServer(server, false);
                return RedirectToAction("Index", "Disclone");
            }
            return View();
        }
        [Authorize]
        public ActionResult Manage()
        {
            var id = this.User.Identity.GetUserId();
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.First(u => u.Id == id);

            if (user == null)
            {
                HttpNotFound();
            }
            List<ServerVM> servers = null;
            try
            {
                Session["DiscloneUserId"] = user.DiscloneUserID;
                
                servers = discloneServerManager.GetUserServerList(user.DiscloneUserID);
            }
            catch (Exception)
            {
                return new HttpNotFoundResult();
            }
            return View(servers);
        }
        public ActionResult Edit(int id)
        {
            List<ServerVM> servers = null;
            try
            {
                int userID = (int)Session["DiscloneUserId"];

                servers = discloneServerManager.GetUserServerList(userID);
                var server = servers.First(s => s.ServerID == id);
                return View(server);
            }
            catch (Exception)
            {
                return new HttpNotFoundResult();
            }
        }
        [HttpPost]
        public ActionResult Edit(ServerVM model)
        {
            try
            {
                int userID = (int)Session["DiscloneUserId"];
                discloneServerManager.EditServer(model, userID, false);
                return RedirectToAction("Manage");
            }
            catch (Exception)
            {
                return new HttpNotFoundResult();
            }
        }
        public ActionResult JoinServer()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult JoinServer(ServerVM server)
        {

                    var id = this.User.Identity.GetUserId();
                    var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    var user = userManager.Users.First(u => u.Id == id);


            IUserManager discloneUserManager = new UserManager();
            try
            {
                discloneUserManager.AddUserToServerWithTag(user.DiscloneUserID, server.Tag);
                return RedirectToAction("Index", "Disclone");
            }
            catch (Exception)
            {

                return new HttpNotFoundResult();
            }
           


                
        }
    }
}