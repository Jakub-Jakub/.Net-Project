using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IMessageAccessor
    {
        List<MessageVM> SelectMessagesByChatroomID(int chatroomId);
        int InsertChatroomMessage(int chatroomId, int userId, string message);
    }
}
