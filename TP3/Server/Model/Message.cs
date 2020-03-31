using IFT585_TP3.Common;
using IFT585_TP3.Server.Repositories;

namespace IFT585_TP3.Server.Model
{
    public class Message : IDeepClonable<Message>
    {
        [Id]
        public string Id { get; set; }

        public string SenderUsername { get; set; }

        public string Content { get; set; }

        public Message DeepClone()
        {
            Message clone = new Message
            {
                Id = Id,
                SenderUsername = SenderUsername,
                Content = Content
            };

            return clone;
        }
    }
}
