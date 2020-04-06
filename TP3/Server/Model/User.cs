using IFT585_TP3.Common;
using IFT585_TP3.Common.UdpServer;
using IFT585_TP3.Server.Repositories;
using System;

namespace IFT585_TP3.Server.Model
{
    public class User : IDeepClonable<User>
    {
        [Id]
        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public DateTime LastActivity { get; set; }

        public User() { }
        public User(Credential creds)
        {
            var adminSalt = PasswordHelper.GenerateSalt();
            Username = creds.userName;
            PasswordSalt = adminSalt;
            PasswordHash = PasswordHelper.Hash(creds.password, adminSalt);
        }

        public User DeepClone()
        {
            User clone = new User
            {
                Username = Username,
                PasswordHash = PasswordHash,
                PasswordSalt = PasswordSalt,
                LastActivity = LastActivity
            };

            return clone;
        }
    }
}
