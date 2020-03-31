using IFT585_TP3.Common;
using IFT585_TP3.Server.Repositories;

namespace IFT585_TP3.Server.Model
{
    public class User : IDeepClonable<User>
    {
        [Id]
        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public User DeepClone()
        {
            User clone = new User
            {
                Username = Username,
                PasswordHash = PasswordHash
            };

            return clone;
        }
    }
}
