using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IFT585_TP3.Server.RESTFramework
{
    public class Response
    {
        private HttpListenerResponse listenerResponse;

        public bool IsClosed { get; private set; }

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
            await SendResponse(serialized, "application/json");
        }

        /// <summary>
        /// Close the request, sending an empty body with status 200.
        /// </summary>
        public void Close()
        {
            listenerResponse.StatusCode = 200;
            listenerResponse.Close();
            IsClosed = true;
        }

        public async Task InternalError(string message)
        {
            await HandleError(500, message);
        }

        public async Task BadRequest(string message)
        {
            await HandleError(400, message);
        }

        public async Task Unauthorized(string message)
        {
            await HandleError(403, message);
        }

        private async Task HandleError(int statusCode, string message)
        {
            await SendResponse(message, "text/plain", statusCode);
        }

        private async Task SendResponse(string content, string contentType, int statusCode = 200)
        {
            var data = Encoding.UTF8.GetBytes(content);

            listenerResponse.ContentType = contentType;
            listenerResponse.ContentEncoding = Encoding.UTF8;
            listenerResponse.ContentLength64 = data.LongLength;
            listenerResponse.StatusCode = statusCode;

            await listenerResponse.OutputStream.WriteAsync(data, 0, data.Length);
            listenerResponse.Close();

            IsClosed = true;
        }
    }
}
