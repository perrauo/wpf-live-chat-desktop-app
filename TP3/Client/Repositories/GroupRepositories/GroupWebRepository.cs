using IFT585_TP3.Client.NetworkFramework;
using IFT585_TP3.Client.Model;
using System;
using System.Collections.Generic;

namespace IFT585_TP3.Client.Repositories.GroupRepositories
{
    //template for send REST request
    public class GroupWebRepository : AbstractRepository<Group>
    {
        private Connection _connection;

        public  void Connect(Connection connection)
        {
            _connection = connection;
        }

        public override void Create(Group toCreate)
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

        public override Group Retrieve(object id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Group> RetrieveAll()
        {
            throw new NotImplementedException();
        }

        public override void Update(Group toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
