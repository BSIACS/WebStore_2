using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Domain.Identity;
using WebStore.Interfaces.Services.Identity;

namespace WebStore.Clients.Identity
{
    public class RolesClient : BaseClient, IRoleClient
    {
        public RolesClient(IConfiguration configuration) : base(configuration, "/api/RolesApi")
        {
        }

        #region IRoleStore<Role>

        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancel) =>
            await (await PostAsync(Address, role, cancel).ConfigureAwait(false))
               .Content
               .ReadAsAsync<bool>(cancel)
                ? IdentityResult.Success
                : IdentityResult.Failed();

        public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancel) =>
            await (await PutAsync(Address, role, cancel).ConfigureAwait(false))
               .Content
               .ReadAsAsync<bool>(cancel)
                ? IdentityResult.Success
                : IdentityResult.Failed();

        public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancel) =>
            await (await PostAsync($"{Address}/Delete", role, cancel).ConfigureAwait(false))
               .Content
               .ReadAsAsync<bool>(cancel)
                ? IdentityResult.Success
                : IdentityResult.Failed();

        public async Task<string> GetRoleIdAsync(Role role, CancellationToken cancel) =>
            await (await PostAsync($"{Address}/GetRoleId", role, cancel).ConfigureAwait(false))
               .Content
               .ReadAsAsync<string>(cancel);

        public async Task<string> GetRoleNameAsync(Role role, CancellationToken cancel) =>
            await (await PostAsync($"{Address}/GetRoleName", role, cancel).ConfigureAwait(false))
               .Content
               .ReadAsAsync<string>(cancel);

        public async Task SetRoleNameAsync(Role role, string name, CancellationToken cancel)
        {
            var response = await PostAsync($"{Address}/SetRoleName/{name}", role, cancel).ConfigureAwait(false);
            role.Name = await response.Content.ReadAsAsync<string>(cancel);
        }

        public async Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancel) =>
            await (await PostAsync($"{Address}/GetNormalizedRoleName", role, cancel).ConfigureAwait(false))
               .Content
               .ReadAsAsync<string>(cancel);

        public async Task SetNormalizedRoleNameAsync(Role role, string name, CancellationToken cancel)
        {
            var response = await PostAsync($"{Address}/SetNormalizedRoleName/{name}", role, cancel).ConfigureAwait(false);
            role.NormalizedName = await response.Content.ReadAsAsync<string>(cancel);
        }

        public async Task<Role> FindByIdAsync(string id, CancellationToken cancel) =>
            await GetAsync<Role>($"{Address}/FindById/{id}", cancel)
               .ConfigureAwait(false);

        public async Task<Role> FindByNameAsync(string name, CancellationToken cancel) =>
            await GetAsync<Role>($"{Address}/FindByName/{name}", cancel)
               .ConfigureAwait(false);

        #endregion
    }
}
