using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IMessageManager
    {
        List<MessageVM> GetChatroomMessages(int chatroomID);
        bool AddChatroomMessage(int chatroomId, int userId, string message);
    }
}
