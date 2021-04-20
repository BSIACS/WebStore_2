using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Clients.Identity
{
    class UsersClient : BaseClient
    {
        public UsersClient(IConfiguration configuration) : base(configuration, "/api/UsersApi")
        {
        }
    }
}
