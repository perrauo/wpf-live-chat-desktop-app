using IFT585_TP3.Client.Model;
using System.Collections.Generic;
using IFT585_TP3.Client.NetworkFramework;

namespace IFT585_TP3.Client.Repositories.GroupRepositories
{
    interface IGroupRepository
    {
        void Connect(Connection connection);

        void Create(Group toCreate);

        Group Retrieve(object id);

        IEnumerable<Group> RetrieveAll();

        void Update(Group toUpdate);

        void Delete(object id);

        bool Exists(object id);
    }
}
