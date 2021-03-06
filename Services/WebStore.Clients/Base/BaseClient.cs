using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebStore.Clients
{
    public abstract class BaseClient : IDisposable
    {
        protected string Address { get; }

        protected HttpClient Http { get; }

        protected BaseClient(IConfiguration configuration, string ServiceAddress) {
            
            Address = ServiceAddress;

            Http = new HttpClient {
                BaseAddress = new Uri(configuration["WebApiUrl"]),
                DefaultRequestHeaders = {
                    Accept = { new MediaTypeWithQualityHeaderValue("application/json")}
                }
            };
        }

        #region Get
        protected async Task<T> GetAsync<T>(string uri, CancellationToken cancel = default) {
            HttpResponseMessage response = await Http.GetAsync(uri, cancel);

            T retVal = await response.EnsureSuccessStatusCode().Content.ReadAsAsync<T>(cancel);

            return retVal;
        }
        
        protected T Get<T>(string uri) => GetAsync<T>(uri).Result;
        #endregion

        #region Post
        protected async Task<HttpResponseMessage> PostAsync<T>(string uri, T t, CancellationToken cancel = default)
        {
            HttpResponseMessage response = await Http.PostAsJsonAsync(uri, t, cancel);

            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Post<T>(string uri, T t) => PostAsync<T>(uri, t).Result;
        #endregion

        #region Put
        protected async Task<HttpResponseMessage> PutAsync<T>(string uri, T t, CancellationToken cancel = default) 
        {
            HttpResponseMessage response = await Http.PutAsJsonAsync(uri, t, cancel);

            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Put<T>(string uri, T t) => PutAsync<T>(uri, t).Result;
        #endregion

        #region Delete
        protected async Task<HttpResponseMessage> DeleteAsync(string uri, CancellationToken cancel = default)
        {
            HttpResponseMessage response = await Http.DeleteAsync(uri, cancel);

            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Delete(string uri) => DeleteAsync(uri).Result;
        #endregion

        public void Dispose() {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
                Http.Dispose();
        }
    }
}
