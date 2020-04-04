using IFT585_TP3.Client.NetworkFramework;
using IFT585_TP3.Client.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IFT585_TP3.Client.Repositories.GroupRepositories
{
    //template for send REST request
    public class GroupWebRepository : AbstractRepository<Group>
    {
        public GroupWebRepository():base()
        {
            
        }

        protected override async Task<Uri> CreateTask(Group obj)
        {
            HttpResponseMessage response = await _client.PostAsync(
                "api/products", null);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        protected override Task<Group> RetrieveTask(object id)
        {
            throw new NotImplementedException();
        }

        protected override Task<IEnumerable<Group>> RetrieveAllTask()
        {
            throw new NotImplementedException();
        }

        protected override Task<Uri> UpdateTask(Group toUpdate)
        {
            throw new NotImplementedException();
        }

        protected override Task<Uri> DeleteTask(object id)
        {
            throw new NotImplementedException();
        }

        protected override Task<bool> ExistsTask(object id)
        {
            throw new NotImplementedException();
        }
    }
}
