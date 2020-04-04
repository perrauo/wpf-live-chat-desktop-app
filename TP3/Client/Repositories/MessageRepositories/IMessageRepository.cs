using IFT585_TP3.Client.Model;
using System.Collections.Generic;
using IFT585_TP3.Client.NetworkFramework;

namespace IFT585_TP3.Client.Repositories.MessageRepositories
{
    interface IMessageRepository
    {
        void Connect(Connection connection);

        void Create(Message toCreate);

        Message Retrieve(object id);

        IEnumerable<Message> RetrieveAll();

        void Update(Message toUpdate);

        void Delete(object id);

        bool Exists(object id);
    }
}
