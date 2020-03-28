using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IFT585_TP3.Server.Framework
{
    public class Response
    {
        private HttpListenerResponse listenerResponse;

        public Response(HttpListenerResponse listenerResponse)
        {
            this.listenerResponse = listenerResponse;
        }

        /// <summary>
        /// Send an object as Json response and terminate the connection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        public async Task Json<ModelType>(ModelType content)
        {
            var serialized = JsonConvert.SerializeObject(content);
            var data = Encoding.UTF8.GetBytes(serialized);

            listenerResponse.ContentType = "application/json";
            listenerResponse.ContentEncoding = Encoding.UTF8;
            listenerResponse.ContentLength64 = data.LongLength;

            await listenerResponse.OutputStream.WriteAsync(data, 0, data.Length);
            listenerResponse.Close();
        }
    }
}
