using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IServerManager
    {
        List<ServerVM> GetUserServerList(int userId);
        bool EditServer(ServerVM server, int userId, bool updateImage);

        bool AddServer(ServerVM server, bool hasImage);
    }
}
