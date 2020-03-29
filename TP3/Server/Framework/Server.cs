using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IFT585_TP3.Server.Framework
{
    public class Server
    {
        private HttpListener listener;
        private List<RequestHandler> handlers = new List<RequestHandler>();

        /// <summary>
        /// Declare a route handler. 
        /// The associated middlewares are executed sequentially until a response is sent.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="urlPattern"></param>
        /// <param name="Middlewares"></param>
        public void Use(Method method, string urlPattern, params Func<Request, Response, Task>[] Middlewares)
        {
            handlers.Add(new RequestHandler
            {
                Method = method,
                UrlPattern = urlPattern,
                Middlewares = Middlewares
            });
        }

        /// <summary>
        /// Start the HTTP server with the declared route handlers at the specified address
        /// </summary>
        /// <param name="address"></param>
        public void Listen(string address)
        {
            listener = new HttpListener();
            listener.Prefixes.Add(address);

            StartServer().GetAwaiter().GetResult();
        }

        private async Task StartServer()
        {
            listener.Start();

            while (true)
            {
                // wait for a request to arrive
                HttpListenerContext context = await listener.GetContextAsync();

                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                // find a handlers matching the method and url
                var handler = handlers.Find(_handler =>
                    _handler.Method.ToString() == request.HttpMethod &&
                    _handler.IsMatch(request.Url.AbsolutePath)
                );

                if (handler == null)
                {
                    Handle404(response);
                }
                else
                {
                    var requestWrapper = new Request(request, handler.ParamsTokens);
                    var responseWrapper = new Response(response);

                    foreach (var middleware in handler.Middlewares)
                    {
                        if (responseWrapper.IsClosed)
                        {
                            continue;
                        }
                        await middleware(
                            requestWrapper,
                            responseWrapper
                        );
                    }
                }
            }
        }

        private void Handle404(HttpListenerResponse response)
        {
            // TODO: we could potentially add a message in the response body
            response.StatusCode = 404;
            response.Close();
        }



        private class RequestHandler
        {
            private string _regexString;
            private string _urlPattern;
            private List<Tuple<string, int>> _paramsTokens = new List<Tuple<string, int>>();

            public Method Method { get; set; }

            public string UrlPattern
            {
                get
                {
                    return _urlPattern;
                }
                set
                {
                    var baseUrlTokenRE = new Regex("[a-zA-Z0-9-._~\\%]*");
                    var tokenReplacementRE = new Regex(":[a-zA-Z0-9-._~]*");

                    var urlTokens = baseUrlTokenRE.Matches(value);
                    for (var index = 0; index < urlTokens.Count; index++)
                    {
                        Match token = urlTokens[index];
                        if (tokenReplacementRE.IsMatch(token.Value))
                        {
                            _paramsTokens.Add(new Tuple<string, int>(token.Value.Substring(1), index));
                        }
                    }

                    _regexString = "^" + tokenReplacementRE.Replace(value, "[a-zA-Z0-9-._~\\%]*") + "$";
                    _urlPattern = value;
                }
            }

            /// <summary>
            /// Collection of params names and their position inside the base url
            /// <br/><br/>
            /// ex: For this path: "/api/group/:group_name/invite/:username", <br/>
            /// the params are {"group_name", 2} and {"username", 4}
            /// </summary>
            public List<Tuple<string, int>> ParamsTokens
            {
                get
                {
                    return _paramsTokens;
                }
            }

            public Func<Request, Response, Task>[] Middlewares { get; set; }

            public bool IsMatch(string url)
            {
                return new Regex(_regexString).IsMatch(url);
            }
        }
    }
}
