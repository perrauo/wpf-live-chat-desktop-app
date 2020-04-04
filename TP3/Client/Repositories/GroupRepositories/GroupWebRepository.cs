using IFT585_TP3.Client.NetworkFramework;
using IFT585_TP3.Client.Model;
using System;
using System.Collections.Generic;
using System.Net;
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
            var objContent = new StringContent(obj.ToString());
            HttpResponseMessage response = await _client.PostAsync("/api/group", objContent);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        protected override async Task<Group> RetrieveTask(object id)
        {
            Group group = null;
            var response = await _client.GetAsync($"/api/group/{id}");
            if (response.IsSuccessStatusCode)
            {
                var roup = await response.Content.ReadAsStringAsync();
            }
            // return URI of the created resource.
            return group;
            
        }

        protected override async Task<IEnumerable<Group>> RetrieveAllTask()
        {
            IEnumerable<Group> group = null;
            var response = await _client.GetAsync("/api/group");
            if (response.IsSuccessStatusCode)
            {
                var roup = await response.Content.ReadAsStringAsync();
            }
            // return URI of the created resource.
            return group;
        }

        protected override async Task<Uri> UpdateTask(Group toUpdate)
        {
            var objContent = new StringContent(toUpdate.ToString());

            if (toUpdate.GroupName != null)
            {
                HttpResponseMessage response = await _client.PutAsync(
                    $"api/products/{toUpdate.GroupName}", objContent);
                response.EnsureSuccessStatusCode();

                // Deserialize the updated product from the response body.
                var stoUpdate = await response.Content.ReadAsStringAsync();
                return response.Headers.Location;
            }

            return null;
        }

        protected override async Task<HttpStatusCode> DeleteTask(object id)
        {
            HttpResponseMessage response = await _client.DeleteAsync(
                $"api/products/{id}");
            return response.StatusCode;
        }

        protected override Task<bool> ExistsTask(object id)
        {
            throw new NotImplementedException();
        }
    }
}
