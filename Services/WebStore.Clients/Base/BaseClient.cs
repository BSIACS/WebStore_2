using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace WebStore.Clients
{
    public abstract class BaseClient
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
    }
}
