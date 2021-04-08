using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SharpBank.Utils
{
    public class Hasher
    {

        public static string ComputeB64HashWithSha256(string unecryptedText)
        {
            Byte[] salt = GenerateSalt(Constants.kSaltSize);
            return ComputeB64Hash(unecryptedText, SHA256.Create(), salt);
        }

        private static string ComputeB64Hash(string unencryptedText, HashAlgorithm algorithm, Byte[] salt)
        {
            Byte[] inBytes = Encoding.UTF8.GetBytes(unencryptedText);
            Byte[] saltedInBytes = new Byte[salt.Length + inBytes.Length];

            salt.CopyTo(saltedInBytes, salt.Length);
            Byte[] hashedBytes = algorithm.ComputeHash(saltedInBytes);

            return Convert.ToBase64String(hashedBytes, 0, hashedBytes.Length);
        }

        private static Byte[] GenerateSalt(int saltSize)
        {
            var chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&".ToCharArray();
            var rand = new Random();

            String saltString = new string("");
            for(int i=0; i<saltSize; i++)
                saltString.Append(chars[rand.Next(0, chars.Length)]);

            return Encoding.UTF8.GetBytes(saltString);
        }

    }
}
