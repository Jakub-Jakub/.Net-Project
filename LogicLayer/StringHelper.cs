using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace LogicLayer
{
    public static class StringHelpers
    {
        //From camp_management
        public static string hashSHA256(this string source)
        {
            string result = "";

            // create a byte array - cryptography is byte oriented
            byte[] data;

            // create a .NET hash provider
            using (SHA256 sha256hash = SHA256.Create())
            {
                data = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(source));
            }
            // build a result string
            var s = new StringBuilder();
            // loop through the data
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }
            result = s.ToString();

            return result;
        }
    }
}
