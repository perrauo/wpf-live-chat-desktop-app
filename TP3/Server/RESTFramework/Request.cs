using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace IFT585_TP3.Server.RESTFramework
{
    public class Request
    {
        private readonly HttpListenerRequest listenerRequest;

        public Request(HttpListenerRequest listenerRequest, List<Tuple<string, int>> paramsTokens)
        {
            this.listenerRequest = listenerRequest;

            Params = new NameValueCollection();
            foreach (var token in paramsTokens)
            {
                var matches = new Regex("(?!\\/)[a-zA-Z0-9-._~\\%]*").Matches(BaseUrl);
                var tokenValue = matches[token.Item2].Value;
                Params.Add(token.Item1, HttpUtility.UrlDecode(tokenValue));
            }
        }

        public string BaseUrl
        {
            get { return listenerRequest.Url.AbsolutePath; }
        }

        public NameValueCollection Params { get; }

        public NameValueCollection Query
        {
            get { return listenerRequest.QueryString; }
        }

        public RootContext Context { get; set; } = new RootContext();

        public async Task<T> GetBody<T>()
        {
            var data = new byte[listenerRequest.ContentLength64];
            await listenerRequest.InputStream.ReadAsync(data, 0, data.Length);

            var content = Encoding.UTF8.GetString(data);
            return JsonConvert.DeserializeObject<T>(content);
        }

        public NameValueCollection Headers
        {
            get { return listenerRequest.Headers; }
        }

        public override string ToString()
        {
            if (Context?.AuthenticatedUser != null)
            {
                return $"user: {Context.AuthenticatedUser.Username}, url: {listenerRequest.HttpMethod} {listenerRequest.Url}";
            }
            return $"url: {listenerRequest.HttpMethod} {listenerRequest.Url}";
        }
    }
}
