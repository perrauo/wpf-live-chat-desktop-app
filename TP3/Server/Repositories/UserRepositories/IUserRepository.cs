using IFT585_TP3.Server.Model;
using System.Collections.Generic;

namespace IFT585_TP3.Server.Repositories.UserRepositories
{
    interface IUserRepository
    {
        void Create(User toCreate);

        User Retrieve(object id);

        IEnumerable<User> RetrieveAll();

        void Update(User toUpdate);

        void Delete(object id);

        bool Exists(object id);
    }
}
