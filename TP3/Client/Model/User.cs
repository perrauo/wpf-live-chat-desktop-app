
using IFT585_TP3.Common.UdpServer;
using System;

namespace IFT585_TP3.Client.Model
{
    public class User
    {
        public string Username { get; set; }

        public string PasswordHash { get; set; }


        public Boolean validation(Credential credentials )
        {
            if (credentials.userName.Equals(this.Username) || credentials.password.Equals(this.PasswordHash))
            {
                return true;
            }
            else
                return false; 
        }
    }
}
