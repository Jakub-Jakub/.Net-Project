using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LogicLayer
{
    public class ServerManager : IServerManager
    {
		private IServerAccessor _serverAccesor;

		public ServerManager()
		{
			_serverAccesor = new ServerAccessor();
		}
		public ServerManager(IServerAccessor serverAccesor)
		{
			_serverAccesor = serverAccesor;
		}
        public List<ServerVM> GetUserServerList(int userId)
        {
			List<ServerVM> servers = null;
			try
			{
				servers = _serverAccesor.SelectServersByUserID(userId);
				foreach (var server in servers)
				{					
					if (server.ServerImage != null)
					{
						ImageSource image;
						image = ImageHelper.ConvertByteArrayToImageSource(server.ServerImage);
						server.ServerImageSource = image;
					}					
				}
			}
			catch (Exception ex)
			{

				throw ex;
			}
			return servers;
        }

		public bool EditServer(ServerVM server, int userId, bool updateImage)
		{
			bool result = false;
			try
			{
				result = 0 != _serverAccesor.UpdateServer(server, userId, updateImage);
			}
			catch (Exception ex)
			{

				throw ex;
			}

			return result;
		}

		public bool AddServer(ServerVM server, bool hasImage)
		{
			bool result = false;

			try
			{
				result = 0 != _serverAccesor.InsertServer(server, hasImage);
			}
			catch (Exception ex)
			{

				throw ex;
			}

			return result;
		}
	}
}
