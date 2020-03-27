using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace IFT585_TP3.Common
{
    public enum Status
    {
        Unknown,
        ConnectionError,
        NonExistentItem,

        Login_InvalidPasswordError,
        Login_InvalidUsernameError,

        Login_AlreadyExistUsernameError,

        Success
    }

    public class Result<T>
    {
        public Status Status { get; set; } = Status.Unknown;
        public T Return { get; set; }
    }

    public static class Utils
    {

        public static byte[] ToASCII(this string val) => Encoding.ASCII.GetBytes(val);

        // https://immortalcoder.blogspot.com/2015/11/best-way-to-secure-password-using-cryptographic-algorithms-in-csharp-dotnet.html
        public static byte[] GenerateSalt()
        {
            const int saltLength = 32;

            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[saltLength];
                randomNumberGenerator.GetBytes(randomNumber);

                return randomNumber;
            }
        }

        private static byte[] Combine(byte[] first, byte[] second)
        {
            var ret = new byte[first.Length + second.Length];

            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);

            return ret;
        }

        public static byte[] HashPasswordWithSalt(byte[] toBeHashed, byte[] salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var combinedHash = Combine(toBeHashed, salt);

                return sha256.ComputeHash(combinedHash);
            }
        }
    }
}
