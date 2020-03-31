using IFT585_TP3.Client.NetworkFramework;
using IFT585_TP3.Client.Model;
using System;
using System.Collections.Generic;

namespace IFT585_TP3.Client.Repositories.MessageRepositories
{
    public class MessageWebRepository : IMessageRepository
    {
        private Connection Connection => throw new NotImplementedException();

        public void Create(Message toCreate)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(object id)
        {
            throw new NotImplementedException();
        }

        public Message Retrieve(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Message> RetrieveAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Message toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
