using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IChatroomAccessor
    {
        List<Chatroom> SelectChatroomsByServerID(int serverid);
        int InsertChatroom(int serverId, string name);
    }
}
