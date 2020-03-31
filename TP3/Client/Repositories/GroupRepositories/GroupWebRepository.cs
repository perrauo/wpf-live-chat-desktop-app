using IFT585_TP3.Client.NetworkFramework;
using IFT585_TP3.Client.Model;
using System;
using System.Collections.Generic;

namespace IFT585_TP3.Client.Repositories.GroupRepositories
{
    public class GroupWebRepository : IGroupRepository
    {
        private Connection Connection => throw new NotImplementedException();

        public void Create(Group toCreate)
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

        public Group Retrieve(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> RetrieveAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Group toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
