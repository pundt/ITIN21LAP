using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace hearthstone.logic
{
    public class Tools
    {
        private static Random random = new Random();

        public static byte[]  GetHash(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));

            SHA512 sha = SHA512.Create();
            UTF8Encoding enc = new UTF8Encoding();
            byte[] bytes = enc.GetBytes(text);

            return sha.ComputeHash(bytes);
        }
        
        /// <summary>
        /// Generates a random string (salt) with given length
        /// </summary>
        /// <param name="length">the length of the generated string</param>
        /// <returns>salt</returns>
        public static string GenerateSalt(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
