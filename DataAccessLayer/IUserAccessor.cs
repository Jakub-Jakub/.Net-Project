using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IUserAccessor
    {
        int VerifyUsernameAndPassword(string username, string passwordHash);
        UserVM SelectUserByEmail(string email);
        int UpdatePassword(string email, string newPasswordHash,
            string oldPasswordHash);
        int InsertNewUserAccount(string email, string userName, string passwordHash);
        int AddUserToServerWithTag(int userId, string tag);
        int UpdateUserImage(int userID, byte[] userImage);
        List<UserVM> SelectUsersByServerID(int serverId);
        int RemoveUserFromServerUserList(int userId, int serverId);
        List<UserVM> SelectAllUsers();
        int UpdateUserIsAdmin(int userId, bool isAdmin);
    }
}
