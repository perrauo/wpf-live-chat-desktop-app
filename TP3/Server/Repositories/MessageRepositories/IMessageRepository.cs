using IFT585_TP3.Server.Model;
using System.Collections.Generic;

namespace IFT585_TP3.Server.Repositories.MessageRepositories
{
    interface IMessageRepository
    {
        void Create(Message toCreate);

        Message Retrieve(object id);

        IEnumerable<Message> RetrieveAll();

        void Update(Message toUpdate);

        void Delete(object id);

        bool Exists(object id);
    }
}
