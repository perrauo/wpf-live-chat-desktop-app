using IFT585_TP3.Client.NetworkFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IFT585_TP3.Client.Controllers
{
    public class Controller
    {
        protected Connection connection;

        public Controller(Connection connection)
        {
            this.connection = connection;
        }

        protected async Task<Result<ResultType>> Get<ResultType>(string relativeUrl)
        {
            using (var client = NetworkHelper.GetClient(connection))
            {
                var response = await client.GetAsync(relativeUrl);
                return await HandleResponse<ResultType>(response);
            }
        }

        protected async Task<Result> Post(string relativeUrl, StringContent content = null)
        {
            using (var client = NetworkHelper.GetClient(connection))
            {
                if (content == null)
                {
                    content = new StringContent("");
                }
                var response = await client.PostAsync(relativeUrl, content);
                return await HandleResponse(response);
            }
        }

        protected async Task<Result<ResultType>> Post<ResultType>(string relativeUrl, StringContent content = null)
        {
            using (var client = NetworkHelper.GetClient(connection))
            {
                if (content == null)
                {
                    content = new StringContent("");
                }
                var response = await client.PostAsync(relativeUrl, content);
                return await HandleResponse<ResultType>(response);
            }
        }

        protected async Task<Result> Put(string relativeUrl, StringContent content = null)
        {
            using (var client = NetworkHelper.GetClient(connection))
            {
                if (content == null)
                {
                    content = new StringContent("");
                }
                var response = await client.PutAsync(relativeUrl, content);
                return await HandleResponse(response);
            }
        }

        protected async Task<Result<ResultType>> Put<ResultType>(string relativeUrl, StringContent content = null)
        {
            using (var client = NetworkHelper.GetClient(connection))
            {
                if (content == null)
                {
                    content = new StringContent("");
                }
                var response = await client.PutAsync(relativeUrl, content);
                return await HandleResponse<ResultType>(response);
            }
        }

        protected async Task<Result> Delete(string relativeUrl)
        {
            using (var client = NetworkHelper.GetClient(connection))
            {
                var response = await client.DeleteAsync(relativeUrl);
                return await HandleResponse(response);
            }
        }

        protected async Task<Result<ResultType>> Delete<ResultType>(string relativeUrl)
        {
            using (var client = NetworkHelper.GetClient(connection))
            {
                var response = await client.DeleteAsync(relativeUrl);
                return await HandleResponse<ResultType>(response);
            }
        }

        private async Task<Result<ResultType>> HandleResponse<ResultType>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var valueString = await response.Content.ReadAsStringAsync();
                var returnValue = JsonConvert.DeserializeObject<ResultType>(valueString);
                return new Result<ResultType>()
                {
                    IsSuccess = true,
                    Value = returnValue
                };
            }
            else
            {
                DisplayError(response.StatusCode, await response.Content.ReadAsStringAsync());
                return new Result<ResultType>() { IsSuccess = false };
            }
        }

        private async Task<Result> HandleResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return new Result() { IsSuccess = true };
            }
            else
            {
                DisplayError(response.StatusCode, await response.Content.ReadAsStringAsync());
                return new Result() { IsSuccess = false };
            }
        }

        private void DisplayError(HttpStatusCode statusCode, string message)
        {
            Console.WriteLine($"[{statusCode}] {message}");
            NotificationService.OnNotificationStaticHandler?.Invoke(NotificationType.Error, $"[{statusCode}] {message}");
        }
    }
}
