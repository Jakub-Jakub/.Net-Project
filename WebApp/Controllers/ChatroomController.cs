using DataObjects;
using LogicLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class ChatroomController : Controller
    {
        IChatroomManager discloneChatroomManager = new ChatroomManager();
        // GET: Chatroom
        [ChildActionOnly]
        public PartialViewResult List(int serverId)
        {
            List<Chatroom> chatrooms = null;
            try
            {
                chatrooms = discloneChatroomManager.GetServerChatrooms(serverId);
            }
            catch (Exception)
            {

            }
            return PartialView(chatrooms);
        }
        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                Session["SelectedChatroomId"] = id;
            }
            return RedirectToAction("Index", "Disclone");
        }
        public ActionResult Manage(int? id)
        {
            if (id != null)
            {
                Session["SelectedServerId"] = id;
            }
            List<Chatroom> chatrooms = null;
            try
            {
                chatrooms = discloneChatroomManager.GetServerChatrooms((int)id);
            }
            catch (Exception)
            {

            }
            return View(chatrooms);
        }
        public ActionResult Create()
        {
            return View();
        }
            [Authorize]
        [HttpPost]
        public ActionResult Create(Chatroom chatroom)
        {
            if (ModelState.IsValid)
            {
                if (Session["SelectedServerId"] == null)
                {
                    return View();
                }

                discloneChatroomManager.AddChatroom((int)Session["SelectedServerId"], chatroom.Name);
                return RedirectToAction("Index", "Disclone");
            }
            return View();
        }
        [Authorize]
        public ActionResult Delete(int id, int serverId)
        {
            try
            {
                discloneChatroomManager.RemoveChatroomById(id);
            }
            catch (Exception)
            {

                throw;
            }            
            return RedirectToAction("Manage", new { id = serverId });
        }
    }
}