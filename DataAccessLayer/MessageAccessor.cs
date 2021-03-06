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
    public class MessageAccessor : IMessageAccessor
    {
        public int InsertChatroomMessage(int chatroomId, int userId, string message)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_insert_chatroom_message", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", userId);
            cmd.Parameters.AddWithValue("@ChatroomID", chatroomId);
            cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 2000);
            cmd.Parameters["@Message"].Value = message;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new ApplicationException("The message could not be added. ");
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


            return result;
        }

        public List<MessageVM> SelectMessagesByChatroomID(int chatroomId)
        {
            List < MessageVM > messages = new List<MessageVM>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_messages_by_chatroomID", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ChatroomID", chatroomId);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        var message = new MessageVM()
                        {
                            MessageID = reader.GetInt32(0),
                            UserID = reader.GetInt32(1),
                            CreatedAt = reader.GetDateTime(2),
                            MessageText = reader.GetString(3),
                            hasMedia = reader.GetBoolean(4),
                            isVisible = reader.GetBoolean(5),
                            UserName = reader.GetString(6)
                        };
                        try
                        {
                            message.UserImage = (byte[])reader["UserImage"];
                        }
                        catch (Exception)
                        {

                            message.UserImage = null;
                        }
                        messages.Add(message);
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



            return messages;
        }
    }
}
