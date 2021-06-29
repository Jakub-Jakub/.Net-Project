using System.Collections.Generic;
using DataObjects;
using System.Web.Http;
using LogicLayer;
using System;

namespace WebApp.Controllers
{
    public class MessageWebController : ApiController
    {
        private IMessageManager discloneMessageManager = new MessageManager();

        [HttpGet]
        [ActionName("GetMessages")]
        public IEnumerable<MessageVM> GetMessages(int id)
        {
            List<MessageVM> messages = null;
            try
            {
                messages = discloneMessageManager.GetChatroomMessages(id);
            }
            catch (Exception)
            {

            }
            return messages;
        }
        [HttpPost]
        public void PostMessage(MessageVM message)
        {
            try
            {
                discloneMessageManager.AddChatroomMessage(message.ChatroomId, message.UserID, message.MessageText);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}