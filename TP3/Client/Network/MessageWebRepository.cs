using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFT585_TP3.Client.Model;

namespace IFT585_TP3.Client.Network
{
    public class MessageWebRepository : IRepository<Message>
    {
        public Connection Connection => throw new NotImplementedException();

        public Message Create(Message obj)
        {
            throw new NotImplementedException();
        }

        public Message Delete(Message obj)
        {
            throw new NotImplementedException();
        }

        public Message Exists(Message obj)
        {
            throw new NotImplementedException();
        }

        public Message Retrieve(User obj)
        {
            throw new NotImplementedException();
        }

        public Message Retrieve(Message obj)
        {
            throw new NotImplementedException();
        }

        public Message Update(Message obj)
        {
            throw new NotImplementedException();
        }
    }
}
