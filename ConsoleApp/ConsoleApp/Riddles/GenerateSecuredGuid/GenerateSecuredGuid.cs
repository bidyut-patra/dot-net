using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public class GenerateSecuredGuid : IAlgorithm<string, string>
    {
        public string Compute(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            var secureGuid = String.Empty;
            using (var sha = MD5Cng.Create())
            {
                var hash = sha.ComputeHash(bytes);
                var guid = new Guid(hash);
                secureGuid = guid.ToString();
            }
            return secureGuid;
        }
    }
}
