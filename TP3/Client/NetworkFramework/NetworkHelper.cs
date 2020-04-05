using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IFT585_TP3.Client.NetworkFramework
{
    static public class NetworkHelper
    {
        static public HttpClient GetClient(Connection connection)
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri(connection.URL),
            };
            client.DefaultRequestHeaders.Add("x-access-token", connection.AccessToken);
            return client;
        }

        static public StringContent WrapContent<ContentType>(ContentType body)
        {
            var serializedJson = JsonConvert.SerializeObject(body);
            return new StringContent(serializedJson, Encoding.UTF8, "application/json");
        }
    }
}
