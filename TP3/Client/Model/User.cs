
namespace IFT585_TP3.Client.Model
{
    public class User: IModel
    {
        public string Username { get; set; }

        public string PasswordHash { get; set; }
    }
}
