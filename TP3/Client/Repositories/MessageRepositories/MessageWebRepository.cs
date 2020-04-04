using IFT585_TP3.Client.NetworkFramework;
using IFT585_TP3.Client.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IFT585_TP3.Client.Repositories.MessageRepositories
{
    public class MessageWebRepository : AbstractRepository<Message>
    {
        protected override Task<Uri> CreateTask(Message obj)
        {
            throw new NotImplementedException();
        }

        protected override Task<Message> RetrieveTask(object id)
        {
            throw new NotImplementedException();
        }

        protected override Task<IEnumerable<Message>> RetrieveAllTask()
        {
            throw new NotImplementedException();
        }

        protected override Task<Uri> UpdateTask(Message toUpdate)
        {
            throw new NotImplementedException();
        }

        protected override Task<Uri> DeleteTask(object id)
        {
            throw new NotImplementedException();
        }

        protected override Task<bool> ExistsTask(object id)
        {
            throw new NotImplementedException();
        }
    }
}
