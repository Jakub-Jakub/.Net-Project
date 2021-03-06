using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DataObjects
{
    public class User
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool Active { get; set; }
        public byte[] UserImage { get; set; }

    }

    public class UserVM : User
    {
        public ImageSource UserImageSource { get; set; }
        public bool isModerator { get; set; }
    }
}
