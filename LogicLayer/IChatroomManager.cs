using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IChatroomManager
    {
        List<Chatroom> GetServerChatrooms(int serverid);
        bool AddChatroom(int serverId, string name);
        bool RemoveChatroomById(int chatroomId);
    }
}
