using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Chatroom
    {
        public int ChatroomID { get; set; }
        public int ServerID { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public DateTime LastMessage { get; set; }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
