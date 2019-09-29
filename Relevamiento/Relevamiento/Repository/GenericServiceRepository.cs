using Newtonsoft.Json;
using Relevamiento.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Relevamiento.Repository
{
    public class GenericServiceRepository : IGenericServiceRepository
    {       
        public Task DeleteAsync(string uri, string authToken = "")
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetAsync<T>(string uri, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(uri);
                HttpResponseMessage responseMessage = await httpClient.GetAsync(uri);

                string jsonResult = string.Empty;

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync();

                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }

                //error
                var content = await responseMessage.Content.ReadAsStringAsync();

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public Task<T> PostAsync<T>(string uri, T data, string authToken = "")
        {
            throw new NotImplementedException();
        }

        public Task<R> PostAsync<T, R>(string uri, T data, string authToken = "")
        {
            throw new NotImplementedException();
        }

        public Task<T> PutAsync<T>(string uri, T data, string authToken = "")
        {
            throw new NotImplementedException();
        }

        private HttpClient CreateHttpClient(string authToken)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders
                .Accept.Add(new MediaTypeWithQualityHeaderValue("aplication/json"));

            if (!string.IsNullOrEmpty(authToken))
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", authToken);
            }

            return httpClient;
        }
    }
}
