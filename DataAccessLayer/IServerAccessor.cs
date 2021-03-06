using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public interface IServerAccessor
    {
        List<ServerVM> SelectServersByUserID(int userID);
        int UpdateServer(ServerVM server, int userId, bool updateImage);
        int InsertServer(ServerVM server, bool hasImage);
    }
}
