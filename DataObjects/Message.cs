using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DataObjects
{
    public class Message
    {
        public int MessageID { get; set; }
        public int UserID { get; set; }
        public DateTime CreatedAt { get; set; }
        public string MessageText { get; set; }
        public bool hasMedia { get; set; }
        public bool isVisible { get; set; }

    }
    public class MessageVM : Message
    {
        public byte[] UserImage { get; set; }
        public string UserName { get; set; }
        public ImageSource UserImageSource { get; set; }
    }
}
