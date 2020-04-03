using IFT585_TP3.Client.Model;
using IFT585_TP3.Client.NetworkFramework;
using IFT585_TP3.Client.Repositories;
using System;
using System.Collections.Generic;

namespace IFT585_TP3.Client.Repositories.UserRepositories
{
    public class UserWebRepository : AbstractRepository<User>
    {
        private Connection Connection => throw new NotImplementedException();

        public void Connect(Connection connection)
        {
            throw new NotImplementedException();
        }

        public override void Create(User toCreate)
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

        public override User Retrieve(object id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<User> RetrieveAll()
        {
            throw new NotImplementedException();
        }

        public override void Update(User toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
