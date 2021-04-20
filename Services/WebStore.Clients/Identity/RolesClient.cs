using Microsoft.Extensions.Configuration;

namespace WebStore.Clients.Identity
{
    class RolesClient : BaseClient
    {
        public RolesClient(IConfiguration configuration) : base(configuration, "/api/RolesApi")
        {
        }
    }
}
