using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using DataAccessLayer;
using System.Windows.Media;

namespace LogicLayer
{
    public class UserManager : IUserManager
    {
        // Dependency inversion
        private IUserAccessor userAccessor;

        public UserManager()
        {
            userAccessor = new UserAccessor();
        }

        public UserManager(IUserAccessor uAccessor)
        {
            this.userAccessor = uAccessor;
        }

        public bool AddUser(string email, string userName, string password)
        {
            bool result = false;

            password = password.hashSHA256();

            try
            {
                result = 0 != userAccessor.InsertNewUserAccount(email, userName, password);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        public bool AddUserToServerWithTag(int userId, string tag)
        {
            bool result = false;
            try
            {
                result = 0 != userAccessor.AddUserToServerWithTag(userId, tag);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        public UserVM AuthenticateUser(string username, string password)
        {
            UserVM user = null;

            // hash the password

            password = password.hashSHA256();

            // call the data access method
            try
            {
                if (1 == userAccessor.VerifyUsernameAndPassword(username, password))
                {
                    // call the methods to create a user object and get roles
                    user = userAccessor.SelectUserByEmail(username);

                    if (user.UserImage != null)
                    {
                        ImageSource image;
                        image = ImageHelper.ConvertByteArrayToImageSource(user.UserImage);
                        user.UserImageSource = image;
                    }

                }
                else
                {
                    throw new ApplicationException("Bad Username or Password");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Login Failed.", ex);
            }

            return user;
        }

        public List<UserVM> GetServerUserList(int serverId)
        {
            List<UserVM> users = null;
            try
            {
                users = userAccessor.SelectUsersByServerID(serverId);
                foreach (var user in users)
                {
                    if (user.UserImage != null)
                    {
                        ImageSource image;
                        image = ImageHelper.ConvertByteArrayToImageSource(user.UserImage);
                        user.UserImageSource = image;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return users;
        }

        public bool UpdatePassword(string email, string oldPassword, string newPassword)
        {
            bool result = false;

            try
            {
                oldPassword = oldPassword.hashSHA256();
                newPassword = newPassword.hashSHA256();
                result = (1 == userAccessor.UpdatePassword(
                    email, newPassword, oldPassword));
                if (!result)
                {
                    throw new ApplicationException("Update Failed.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Password not changed.", ex);
            }

            return result;
        }

        public bool UpdateUserImage(int userID, byte[] userImage)
        {
            bool result = false;

            try
            {
                result = 0 != userAccessor.UpdateUserImage(userID, userImage);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result;
        }
    }
}

