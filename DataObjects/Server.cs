using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media;

namespace DataObjects
{
    public class Server
    {
        public int ServerID { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Tag { get; set; }
        public int OwnerUserID { get; set; }
        public string Name { get; set; }
        public DateTime LastMessage { get; set; }
        public byte[] ServerImage { get; set; }

        public override string ToString()
        {
            return Name;
        }

    }
    public class ServerVM : Server
    {
        public ImageSource ServerImageSource { get; set; }
        public string OwnerUserName { get; set; }
    }
}
