using System;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using System.Security;
using System.Reflection;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace SecureStore
{
    public enum SecretForm
    {
        Hashed,
        Encrypted
    }

    public class SecureMemory
    {
        private static Dictionary<string, SecureString> secureStrings;

        internal const string MASTER_KEY = "MSK";
        internal const string USER_KEY = "USK";
        internal const string PASSWORD_KEY = "PSK";

        static SecureMemory()
        {
            secureStrings = new Dictionary<string, SecureString>();
        }

        /// <summary>
        /// Saves the secret into secure memory
        /// </summary>
        /// <param name="key"></param>
        /// <param name="secret"></param>
        /// <param name="secretForm"></param>
        /// <returns></returns>
        public static string Add(string key, string secret, SecretForm secretForm = SecretForm.Hashed)
        {
            if(string.IsNullOrWhiteSpace(secret))
            {
                throw new InvalidMasterKey(Resource.InvalidMasterKey);
            }

            var secretValue = Encoding.UTF8.GetBytes(secret);
            if (secretForm == SecretForm.Hashed)
            {
                secretValue = GetHashValue(secret);
            }

            if (secretForm == SecretForm.Encrypted)
            {
                secretValue = GetEncryptedValue(secret);
            }

            var secretString = BytesToString(secretValue);
            var secureString = new SecureString();
            foreach(char c in secretString)
            {
                secureString.AppendChar(c);
            }

            if(secureStrings.ContainsKey(key))
            {
                secureStrings[key] = secureString;
            }
            else
            {
                secureStrings.Add(key, secureString);
            }

            return secretString;
        }

        /// <summary>
        /// Gets the encrypted value from secure memory
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValue(string key)
        {
            if (secureStrings.ContainsKey(key))
            {
                var secureStr = secureStrings[key];
                var cred = new NetworkCredential(string.Empty, secureStr);
                return cred.Password;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Removes a key-value pair from secure memory
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            if (secureStrings.ContainsKey(key))
            {
                secureStrings.Remove(key);
            }
        }

        /// <summary>
        /// Gets the encrypted value of a given secret
        /// </summary>
        /// <param name="secret"></param>
        /// <returns></returns>
        private static byte[] GetEncryptedValue(string secret)
        {
            var aes = GetAesSecret();
            var encryptedValue = ManagedAesEncryption.Encrypt(secret, aes.Key, aes.IV);
            return encryptedValue;
        }

        /// <summary>
        /// Gets the decrypted value for a given key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetDecryptedValue(string key)
        {
            var aes = GetAesSecret();
            var encryptedSecret = SecureMemory.GetValue(key);
            var encryptedBytes = StringToBytes(encryptedSecret);
            var decryptedValue = ManagedAesEncryption.Decrypt(encryptedBytes, aes.Key, aes.IV);
            return decryptedValue;
        }

        /// <summary>
        /// Gets SHA256 hash in string form
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetHashString(string input)
        {
            var hashBytes = GetHashValue(input);
            var hashString = BytesToString(hashBytes);
            return hashString;
        }

        /// <summary>
        /// Gets SHA256 hash bytes
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static byte[] GetHashValue(string input)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                var byteData = Encoding.UTF8.GetBytes(input);
                return sha256Hash.ComputeHash(byteData);
            }
        }

        /// <summary>
        /// Gets the string form of bytes
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string BytesToString(byte[] bytes)
        {
            var hashString = string.Empty;
            foreach (var hashByte in bytes)
            {
                hashString += hashByte.ToString("X2");
            }
            return hashString;
        }

        /// <summary>
        /// Gets the string form of bytes
        /// </summary>
        /// <param name="hashBytes"></param>
        /// <returns></returns>
        public static byte[] StringToBytes(string text)
        {
            return Enumerable.Range(0, text.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(text.Substring(x, 2), 16))
                             .ToArray();
        }

        /// <summary>
        /// DAT file path
        /// </summary>
        internal static string DbPath
        {
            get
            {
                var product = Assembly.GetExecutingAssembly().GetName().Name;
                var localAppData = Environment.SpecialFolder.LocalApplicationData;
                var productAppData = Environment.GetFolderPath(localAppData) + @"\" + product;
                if (Directory.Exists(productAppData) == false)
                {
                    Directory.CreateDirectory(productAppData);
                }
                return productAppData + @"\" + DbFileName;
            }
        }

        /// <summary>
        /// Database file name
        /// </summary>
        internal static string DbFileName
        {
            get
            {
                var fileName = Assembly.GetExecutingAssembly().GetName().Name;
                return fileName + ".dat";
            }
        }

        /// <summary>
        /// Reads data from DAT file
        /// </summary>
        /// <returns></returns>
        public static byte[] ReadDatabase()
        {
            var contentBytes = new byte[0];
            if(File.Exists(DbPath))
            {                
                var encryptedContent = File.ReadAllBytes(DbPath);
                try
                {
                    var secret = GetAesSecret();
                    var content = ManagedAesEncryption.Decrypt(encryptedContent, secret.Key, secret.IV);
                    if (content == null)
                    {
                        throw new Exception(Resource.DbDataDecryptionError);
                    }
                    else
                    {
                        contentBytes = StringToBytes(content);
                    }
                }
                catch(Exception e)
                {
                    throw new Exception(Resource.DbDataDecryptionError + e.StackTrace);
                }
            }
            return contentBytes;
        }

        /// <summary>
        /// Saves the data to DAT file
        /// </summary>
        /// <param name="data"></param>
        public static void SaveDatabase(byte[] data)
        {
            var secret = GetAesSecret();
            var strData = BytesToString(data);
            try
            {
                var encryptedData = ManagedAesEncryption.Encrypt(strData, secret.Key, secret.IV);
                if (encryptedData == null)
                {
                    throw new Exception(Resource.DbDataEncryptionError);
                }
                else
                {
                    File.WriteAllBytes(DbPath, encryptedData);
                }
            }
            catch(Exception e)
            {
                throw new Exception(Resource.DbDataEncryptionError + e.StackTrace);
            }
        }

        /// <summary>
        /// Generates Key & IV for AES encryption / decryption
        /// </summary>
        /// <returns></returns>
        private static AesSecret GetAesSecret()
        {
            var masterKey = SecureMemory.GetValue(SecureMemory.MASTER_KEY);
            var masterKeyHash = StringToBytes(masterKey);
            return new AesSecret(masterKeyHash);
        }

        /// <summary>
        /// Computes Key & IV from given hash
        /// </summary>
        struct AesSecret
        {
            internal byte[] Key { get; private set; }
            internal byte[] IV { get; private set; }

            internal AesSecret(byte[] keyHash)
            {
                Key = keyHash;
                IV = new byte[16];
                for(int i = 0, j = 0; i < IV.Length; i++, j += 2)
                {
                    IV[i] = keyHash[j];
                }
            }
        }
    }
}
