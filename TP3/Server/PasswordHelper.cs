using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IFT585_TP3.Server
{
    static public class PasswordHelper
    {
        static public byte[] GenerateSalt()
        {
            var random = new RNGCryptoServiceProvider();
            byte[] salt = new byte[32];

            random.GetNonZeroBytes(salt);
            return salt;
        }

        static public string Hash(string password, byte[] salt)
        {
            return Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8
                )
            );
        }
    }
}
