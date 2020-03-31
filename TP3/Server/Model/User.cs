using IFT585_TP3.Server.Repositories;

namespace IFT585_TP3.Server.Model
{
    public class User
    {
        [Id]
        public string Username { get; set; }

        public string PasswordHash { get; set; }
    }
}
