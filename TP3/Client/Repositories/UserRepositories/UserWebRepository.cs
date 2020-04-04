using IFT585_TP3.Client.Model;
using IFT585_TP3.Client.NetworkFramework;
using IFT585_TP3.Client.Repositories;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace IFT585_TP3.Client.Repositories.UserRepositories
{
    public class UserWebRepository : AbstractRepository<User>
    {
        protected override Task<Uri> CreateTask(User obj)
        {
            throw new NotImplementedException();
        }

        protected override Task<User> RetrieveTask(object id)
        {
            throw new NotImplementedException();
        }

        protected override Task<IEnumerable<User>> RetrieveAllTask()
        {
            throw new NotImplementedException();
        }

        protected override Task<Uri> UpdateTask(User toUpdate)
        {
            throw new NotImplementedException();
        }

        protected override Task<HttpStatusCode> DeleteTask(object id)
        {
            throw new NotImplementedException();
        }

        protected override Task<bool> ExistsTask(object id)
        {
            throw new NotImplementedException();
        }
    }
}
