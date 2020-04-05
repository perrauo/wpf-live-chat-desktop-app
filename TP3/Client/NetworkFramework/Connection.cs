
namespace IFT585_TP3.Client.NetworkFramework
{
    public class Connection
    {
        public bool IsAdmin
        {
            get
            {
                return Username == "admin";
            }
        }
        public string Username { get; set; }
        public string AccessToken { get; set; }
    }
}
