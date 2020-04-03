using IFT585_TP3.Client.NetworkFramework;
using IFT585_TP3.Client.Model;
using System;
using System.Collections.Generic;

namespace IFT585_TP3.Client.Repositories.MessageRepositories
{
    public class MessageWebRepository : AbstractRepository<Message>
    {
        private Connection Connection => throw new NotImplementedException();

        public void Connect(Connection connection)
        {
            throw new NotImplementedException();
        }

        public override void Create(Message toCreate)
        {
            throw new NotImplementedException();
        }

        public override void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public override bool Exists(object id)
        {
            throw new NotImplementedException();
        }

        public override Message Retrieve(object id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Message> RetrieveAll()
        {
            throw new NotImplementedException();
        }

        public override void Update(Message toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
