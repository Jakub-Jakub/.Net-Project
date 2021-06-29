using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IUserManager
    {
        UserVM AuthenticateUser(string username, string password);

        bool UpdatePassword(string email, string oldPassword, string NewPassword);
        bool AddUser(string email, string userName, string password);
        bool AddUserToServerWithTag(int userId, string tag);
        bool UpdateUserImage(int userID, byte[] userImage);
        List<UserVM> GetServerUserList(int serverId);
        UserVM GetUserByEmail(string email);
        bool RemoveUserFromServerUserList(int userId, int serverId);
        List<UserVM> SelectAllUsers();
        bool UpdateUserIsAdmin(int userId, bool isAdmin);
    }
}
