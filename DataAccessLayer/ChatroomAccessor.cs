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
    public class ChatroomAccessor : IChatroomAccessor
    {
        public int DeleteChatroomByID(int chatroomId)
        {
            int result = 0;

            // we need a connection object
            var conn = DBConnection.GetDBConnection();

            // next, we need a command object
            var cmd = new SqlCommand("sp_delete_chatroom_by_id", conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // set up the command parameters
            cmd.Parameters.AddWithValue("@ChatroomID", chatroomId);
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

        public int InsertChatroom(int serverId, string name)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_insert_chatroom", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ServerID", serverId);
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100);
            cmd.Parameters["@Name"].Value = name;

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

        public List<Chatroom> SelectChatroomsByServerID(int serverid)
        {
            List<Chatroom> chatrooms = new List<Chatroom>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_chatrooms_by_serverid", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ServerID", serverid);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        var chatroom = new Chatroom()
                        {
                            ChatroomID = reader.GetInt32(0),
                            CreatedAt = reader.GetDateTime(1),
                            Name = reader.GetString(2),
                            LastMessage = reader.GetDateTime(3)
                        };
                        chatroom.ServerID = serverid;
                        chatrooms.Add(chatroom);
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

            return chatrooms;
        }
    }
}
