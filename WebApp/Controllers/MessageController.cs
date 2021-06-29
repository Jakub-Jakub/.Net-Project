using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class MessageController : Controller
    {
        IMessageManager discloneMessageManager = new MessageManager();
        [ChildActionOnly]
        public PartialViewResult List(int chatroomId)
        {
            List<MessageVM> messages = null;
            try
            {
                messages = discloneMessageManager.GetChatroomMessages(chatroomId);
            }
            catch (Exception)
            {

            }
            return PartialView(messages);
        }
    }
}