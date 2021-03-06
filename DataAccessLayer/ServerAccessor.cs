using DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataAccessLayer
{
    public class ServerAccessor : IServerAccessor
    {
        public int InsertServer(ServerVM server, bool hasImage)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_insert_new_server", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", server.OwnerUserID);
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Tag", SqlDbType.NVarChar, 8);
            cmd.Parameters["@Name"].Value = server.Name;
            cmd.Parameters["@Tag"].Value = server.Tag;

            if (hasImage)
            {
                cmd.Parameters.Add("@ServerImage", SqlDbType.VarBinary);
                cmd.Parameters["@ServerImage"].Value = server.ServerImage;
            }
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

        public List<ServerVM> SelectServersByUserID(int userID)
        {
            List<ServerVM> servers = new List<ServerVM>();

            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_select_servers_by_userid", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", userID);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        var server = new ServerVM()
                        {
                            ServerID = reader.GetInt32(0),
                            CreatedAt = reader.GetDateTime(1),
                            Tag = reader.GetString(2),
                            OwnerUserID = reader.GetInt32(3),
                            Name = reader.GetString(4),
                            LastMessage = reader.GetDateTime(5),
                            OwnerUserName = reader.GetString(6)
                            //ServerImage = (byte[])reader["ServerImage"]
                            //ServerImage = (byte[])reader.GetBytes(6, 0, null, 0, Int32.MaxValue);
                        };
                        try
                        {
                            server.ServerImage = (byte[])reader["ServerImage"];
                        }
                        catch (Exception)
                        {

                            server.ServerImage = null;
                        }
                        
                        servers.Add(server);
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

            return servers;
        }

        public int UpdateServer(ServerVM server, int userId, bool updateImage)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();

            var cmd = new SqlCommand("sp_update_server", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", userId);
            cmd.Parameters.AddWithValue("@ServerID", server.ServerID);
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Tag", SqlDbType.NVarChar, 8);
            cmd.Parameters["@Name"].Value = server.Name;
            cmd.Parameters["@Tag"].Value = server.Tag;

            if (updateImage)
            {
                cmd.Parameters.Add("@ServerImage", SqlDbType.VarBinary);
                cmd.Parameters["@ServerImage"].Value = server.ServerImage;
            }
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
    }
}
