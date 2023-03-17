using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_FinalProj
{
    static class Sha256Crypt
    {
        static public string Sha256(string password, string login)
        {
            try
            {
                string passwordSha256 = "";
                password += login[0];
                password += login[login.Length - 1];
                password += login[login.Length / 2];
                password.Reverse();
                SHA256CryptoServiceProvider x1 = new SHA256CryptoServiceProvider();

                byte[] bs1 = System.Text.Encoding.UTF8.GetBytes(password);
                bs1 = x1.ComputeHash(bs1);

                foreach (byte b in bs1)
                {
                    passwordSha256 += (b.ToString("x2").ToLower());
                }
                return passwordSha256;
            }
            catch(Exception ex)
            {
                return "";
            }
        }
    }
}
