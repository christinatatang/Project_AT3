using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Project_MusicPlayer
{
    /*
        Name: Christina Tatang
        ID: 30003663
        DoB: 02/08/2000
        Project - Programming III 
        This is a project about music player application. 
    */
    class HashTech
    {
        public string GetPasswordHash(string message)
        {
            // Let us use SHA256 algorithm to
            // generate the hash from this salted password
            SHA256 sha = new SHA256CryptoServiceProvider();
            byte[] dataBytes = GetBytes(message);
            byte[] resultBytes = sha.ComputeHash(dataBytes);
            // return the hash string to the caller
            return Convert.ToBase64String(resultBytes);
        }

        private static RNGCryptoServiceProvider m_cryptoServiceProvider = new RNGCryptoServiceProvider();
        private const int SALT_SIZE = 20;

        public static string GetSaltString()
        {
            // Lets create a byte array to store the salt bytes
            byte[] saltBytes = new byte[SALT_SIZE];
            // lets generate the salt in the byte array
            m_cryptoServiceProvider.GetNonZeroBytes(saltBytes);
            // Let us get some string representation for this salt
            string saltString = Convert.ToBase64String(saltBytes);
            // Now we have our salt string ready lets return it to the caller
            return saltString;
        }

        public string GeneratePasswordHash(string plainTextPassword, out string salt)
        {
            salt = GetSaltString();
            string finalString = plainTextPassword + salt ;
            return GetPasswordHash(finalString);
        }
        public bool IsPasswordMatch(string password, string salt, string hash)
        {
            string finalString = password + salt ;
            return hash == GetPasswordHash(finalString);
        }


        // utilty function to convert string to byte[]
        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
        // utilty function to convert byte[] to string
        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
    }
}
