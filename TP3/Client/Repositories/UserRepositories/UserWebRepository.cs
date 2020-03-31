using IFT585_TP3.Client.Model;
using IFT585_TP3.Client.NetworkFramework;
using IFT585_TP3.Client.Repositories;
using System;
using System.Collections.Generic;

namespace IFT585_TP3.Client.Repositories.UserRepositories
{
    public class UserWebRepository : IUserRepository
    {
        private Connection Connection => throw new NotImplementedException();

        public void Create(User toCreate)
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

        public User Retrieve(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> RetrieveAll()
        {
            throw new NotImplementedException();
        }

        public void Update(User toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
