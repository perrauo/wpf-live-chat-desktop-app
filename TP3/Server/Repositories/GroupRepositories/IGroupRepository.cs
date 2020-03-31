using IFT585_TP3.Server.Model;
using System.Collections.Generic;

namespace IFT585_TP3.Server.Repositories
{
    interface IGroupRepository
    {
        void Create(Group toCreate);

        Group Retrieve(object id);

        IEnumerable<Group> RetrieveAll();

        void Update(Group toUpdate);

        void Delete(object id);

        bool Exists(object id);
    }
}
