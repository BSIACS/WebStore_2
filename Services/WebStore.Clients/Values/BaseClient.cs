using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace WebStore.Clients.Values
{
    public abstract class BaseClient
    {
        protected string Address { get; }

        protected HttpClient Http { get; }

        protected BaseClient(HttpClient client, string ServiceAddress) {
            Http = client;
            ServiceAddress = Address;
        }
    }
}
