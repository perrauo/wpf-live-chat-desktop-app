using IFT585_TP3.Client.Model;
using System.Collections.Generic;

namespace IFT585_TP3.Client.Repositories.GroupRepositories
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
