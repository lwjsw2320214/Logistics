using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace common
{
    public class LogisticsEncryption
    {

        public string Md5Basece64(string content)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider MD5CSP = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] MD5Source = System.Text.Encoding.UTF8.GetBytes(content);
            byte[] MD5Out = MD5CSP.ComputeHash(MD5Source);
            return Convert.ToBase64String(MD5Out);
        }

    }
}
