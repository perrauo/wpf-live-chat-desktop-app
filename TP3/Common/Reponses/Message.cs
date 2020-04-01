using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFT585_TP3.Common.Reponses
{
    public class Message
    {
        public string Id { get; set; }

        public string SenderUsername { get; set; }

        public string GroupName { get; set; }

        public string Content { get; set; }

        public DateTime Timestamp { get; set; }

    }
}
