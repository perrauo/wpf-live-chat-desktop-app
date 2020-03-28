using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFT585_TP3.Common;

namespace IFT585_TP3.Server
{    
    public class Program
    {
        public static Dictionary<string, byte[]> _users = new Dictionary<string, byte[]>();

        static void Main(string[] args)
        {
            // Add default admin
            _users.Add("admin", Utils.HashPasswordWithSalt("admin".ToASCII(), Utils.GenerateSalt()));
        }
    }
}
