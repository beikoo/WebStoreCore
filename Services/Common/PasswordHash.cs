using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Services.Common
{
    public class PasswordHash
    {
        public string CreateSalt(int size)
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public static string GenerateHash(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input + MessageAndVariables.salt);
            SHA256Managed sHA256ManagedString = new SHA256Managed();
            byte[] hash = sHA256ManagedString.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public bool AreEqual(string plainTextInput, string hashedInput)
        {
            string newHashedPin = GenerateHash(plainTextInput);
            return newHashedPin.Equals(hashedInput);
        }
    }
}
