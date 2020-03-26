using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Client.Model;

namespace Client.Network
{
    public class UserWebRepository : IRepository<User>
    {
        public Connection Connection => throw new NotImplementedException();

        public User Create(User obj)
        {
            throw new NotImplementedException();
        }

        public User Delete(User obj)
        {
            throw new NotImplementedException();
        }

        public User Exists(User obj)
        {
            throw new NotImplementedException();
        }

        public User Retrieve(User obj)
        {
            throw new NotImplementedException();
        }

        public User Update(User obj)
        {
            throw new NotImplementedException();
        }
    }
}
