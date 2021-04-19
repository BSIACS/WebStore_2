using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using WebStore.Interfaces.TestApi;

namespace WebStore.Clients
{
    public class ValuesClient : BaseClient, IValuesService
    {
        public ValuesClient(IConfiguration configuration) : base(configuration, "api/values")
        {

        }

        public IEnumerable<string> Get()
        {
            var response = Http.GetAsync(Address).Result;
            if (response.IsSuccessStatusCode) {
                return response.Content.ReadAsAsync<IEnumerable<string>>().Result;
            }
            return Enumerable.Empty<string>();
        }

        public string Get(int id)
        {
            var response = Http.GetAsync($"{Address}/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<string>().Result;
            }
            return string.Empty;
        }

        public Uri Post(string value)
        {
            var response = Http.PostAsJsonAsync(Address, value).Result;
            return response.EnsureSuccessStatusCode().Headers.Location;
        }

        public HttpStatusCode Update(int id, string value)
        {
            var response = Http.PostAsJsonAsync($"{Address}/{ id}", value).Result;
            return response.EnsureSuccessStatusCode().StatusCode;
        }

        public HttpStatusCode Delete(int id)
        {
            var response = Http.DeleteAsync($"{Address}/{ id}").Result;
            return response.StatusCode;
        }
    }
}
