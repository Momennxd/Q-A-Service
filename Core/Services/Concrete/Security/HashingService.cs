using System;
using System.Security.Cryptography;
using System.Text;

namespace Core.Services.Concrete.Security
{
    /// <summary>
    /// Hashing and compare data
    /// </summary>
    public static class HashingService
    {
        /// <summary>
        /// To hash data on 256 bits
        /// </summary>
        /// <typeparam name="T">any data type</typeparam>
        /// <param name="data"></param>
        /// <returns>hashed data</returns>
        public static string Hash<T>(T data)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] HashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data.ToString()));

                return BitConverter.ToString(HashBytes).Replace("-", "").ToLower();
            }
        }
        /// <summary>
        /// compare the hash to the its value.
        /// </summary>
        /// <typeparam name="T">any data type</typeparam>
        /// <param name="value1">first value to be compared</param>
        /// <param name="value2">second value to be compared</param>
        /// <returns>is equle</returns>
        public static bool IsTheSame<T>(T value1, string sHash)
        {
            return Hash(value1) == sHash;
        }
    }
}