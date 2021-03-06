using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UserAccessor : IUserAccessor
    {
        public UserVM SelectUserByEmail(string email)
        {
            UserVM user = new UserVM();

            // get a connection
            var conn = DBConnection.GetDBConnection();

            // get a command
            var cmd = new SqlCommand("sp_select_user_by_email", conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);

            // values
            cmd.Parameters["@Email"].Value = email;

            // execute the command
            try
            {
                // open the connection
                conn.Open();

                // execute the command
                var reader = cmd.ExecuteReader();

                // process the results
                if (reader.HasRows)
                {
                    reader.Read();

                    user = new UserVM()
                    {
                        UserID = reader.GetInt32(0),
                        UserName = reader.GetString(1),
                        Active = reader.GetBoolean(2),
                        Email = email
                    };
                    try
                    {
                        user.UserImage = (byte[])reader["UserImage"];
                    }
                    catch (Exception)
                    {

                        user.UserImage = null;
                    }

                    reader.Close();               
                }
                else
                {
                    throw new ApplicationException("User not found.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return user;
        }

        public int VerifyUsernameAndPassword(string username, string passwordHash)
        {
            int result = 0; // once authenticated, this will be 1

            // we need a connection object
            var conn = DBConnection.GetDBConnection();

            // next, we need a command object
            var cmd = new SqlCommand("sp_authenticate_user", conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // set up the command parameters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            // set the parameter values
            cmd.Parameters["@Email"].Value = username;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            // now that the command is set up, we need to execute it
            // all database code is unsafe, so it goes in a try block

            try
            {
                // open the connection
                conn.Open();

                // execute the command and capture the result
                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public int UpdatePassword(string email, string newPasswordHash, string oldPasswordHash)
        {
            int result = 0;

            // connection
            var conn = DBConnection.GetDBConnection();
            // command
            var cmd = new SqlCommand("sp_update_passwordhash", conn);
            // command type
            cmd.CommandType = CommandType.StoredProcedure;
            // parameters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldPasswordHash", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewPasswordHash", SqlDbType.NVarChar, 100);
            // values
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@OldPasswordHash"].Value = oldPasswordHash;
            cmd.Parameters["@NewPasswordHash"].Value = newPasswordHash;

            // run the command
            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        public int InsertNewUserAccount(string email, string userName, string passwordHash)
        {
            int result = 0;

            // we need a connection object
            var conn = DBConnection.GetDBConnection();

            // next, we need a command object
            var cmd = new SqlCommand("sp_insert_new_user_account", conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // set up the command parameters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            // set the parameter values
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@UserName"].Value = userName;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;


            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        public int AddUserToServerWithTag(int userId, string tag)
        {
            int result = 0;

            // we need a connection object
            var conn = DBConnection.GetDBConnection();

            // next, we need a command object
            var cmd = new SqlCommand("sp_add_user_to_server_user_list_by_tag", conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // set up the command parameters
            cmd.Parameters.AddWithValue("@UserID", userId);
            cmd.Parameters.Add("@Tag", SqlDbType.NVarChar, 8);            

            // set the parameter values
            cmd.Parameters["@Tag"].Value = tag;
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public int UpdateUserImage(int userID, byte[] userImage)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_update_userimage", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", userID);          

            cmd.Parameters.Add("@UserImage", SqlDbType.VarBinary);
            cmd.Parameters["@UserImage"].Value = userImage;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }



            return result;
        }

        public List<UserVM> SelectUsersByServerID(int serverId)
        {
            List<UserVM> users = new List<UserVM>();

            // get a connection
            var conn = DBConnection.GetDBConnection();

            // get a command
            var cmd = new SqlCommand("sp_select_users_by_serverid", conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.AddWithValue("@ServerID", serverId);

            // execute the command
            try
            {
                // open the connection
                conn.Open();

                // execute the command
                var reader = cmd.ExecuteReader();

                // process the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        UserVM user = new UserVM()
                        {
                            UserID = reader.GetInt32(0),
                            UserName = reader.GetString(1),
                            isModerator = reader.GetBoolean(2)
                        };
                        try
                        {
                            user.UserImage = (byte[])reader["UserImage"];
                        }
                        catch (Exception)
                        {

                            user.UserImage = null;
                        }
                        users.Add(user);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return users;
        }
    }
}
