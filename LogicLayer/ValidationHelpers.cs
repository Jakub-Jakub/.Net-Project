using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public static class ValidationHelpers
    {
        public static bool isTagValid(this string tag)
        {
            bool result = false;

            if(tag.Length == 8)
            {
                result = true;
            }

            return result;
        }

        public static bool isValidServerName(this string serverName)
        {
            bool result = false;

            if (serverName.Length <= 100 && serverName.Length > 0)
            {
                result = true;
            }

            return result;
        }
    }
}
