using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Util.security
{
    public static class MD5Extend
    {
        public static string ToMD5(this string input)
        {
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        public static bool MD5IsEuqls(this string sourceString, string md5Hash)
        {
            string hashOfInput = sourceString.ToMD5();
            return hashOfInput == md5Hash;
        }
    }
}
