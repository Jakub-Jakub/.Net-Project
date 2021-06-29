using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class ChatroomManager : IChatroomManager
    {
        private IChatroomAccessor _chatroomAccessor;
        public ChatroomManager()
        {
            _chatroomAccessor = new ChatroomAccessor();
        }

        public bool AddChatroom(int serverId, string name)
        {
            bool result = false;

            try
            {
                result = 0 != _chatroomAccessor.InsertChatroom(serverId, name);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result;
        }

        public List<Chatroom> GetServerChatrooms(int serverid)
        {
            List<Chatroom> chatrooms = new List<Chatroom>();
            try
            {
                chatrooms = _chatroomAccessor.SelectChatroomsByServerID(serverid);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return chatrooms;
        }

        public bool RemoveChatroomById(int chatroomId)
        {
            bool result = false;

            try
            {
                result = 0 != _chatroomAccessor.DeleteChatroomByID(chatroomId);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result;
        }
    }
}
