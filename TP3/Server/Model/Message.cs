using IFT585_TP3.Common;
using IFT585_TP3.Server.Repositories;
using System;

namespace IFT585_TP3.Server.Model
{
    public class Message : IDeepClonable<Message>
    {
        public string SenderUsername { get; set; }

        public string GroupName { get; set; }

        public string Content { get; set; }

        [Id]
        public DateTime Timestamp { get; set; }

        public Message DeepClone()
        {
            Message clone = new Message
            {
                SenderUsername = SenderUsername,
                GroupName = GroupName,
                Content = Content,
                Timestamp = Timestamp
            };

            return clone;
        }
    }
}
