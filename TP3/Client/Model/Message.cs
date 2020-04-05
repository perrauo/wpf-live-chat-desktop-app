
using System;

namespace IFT585_TP3.Client.Model
{
    public class Message: IModel
    {
        public string SenderUsername { get; set; }

        public string GroupName { get; set; }

        public string Content { get; set; }

        public DateTime Timestamp { get; set; }

    }
}
