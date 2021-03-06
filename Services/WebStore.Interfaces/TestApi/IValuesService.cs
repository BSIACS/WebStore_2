using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace WebStore.Interfaces.TestApi
{
    public interface IValuesService
    {
        IEnumerable<string> Get();

        string Get(int id);

        Uri Post(string value);

        HttpStatusCode Update(int id, string value);

        HttpStatusCode Delete(int id);
    }
}
