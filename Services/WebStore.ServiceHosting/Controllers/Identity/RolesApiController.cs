using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Identity;

namespace WebStore.ServiceHosting.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesApiController : ControllerBase
    {
        private readonly RoleStore<Role> _roleStore;

        public RolesApiController(WebStoreDB dB)
        {
            _roleStore = new RoleStore<Role>(dB);
        }

        [HttpGet("all")]
        public async Task<IEnumerable<Role>> GetAllRoles() {
            return await _roleStore.Roles.ToListAsync();
        }

        [HttpPost]
        public async Task<bool> CreateAsync([FromBody] Role role)
        {
            IdentityResult identityResult = await _roleStore.CreateAsync(role);
            return identityResult.Succeeded;
        }

        [HttpPut]
        public async Task<bool> UpdateAsync([FromBody] Role role)
        {
            IdentityResult identityResult = await _roleStore.UpdateAsync(role);
            return identityResult.Succeeded;
        }

        [HttpDelete]
        public async Task<bool> DeleteAsync([FromBody] Role role)
        {
            IdentityResult identityResult = await _roleStore.DeleteAsync(role);
            return identityResult.Succeeded;
        }

        [HttpPost("GetRoleId")]
        public async Task<string> GetRoleIdAsync([FromBody] Role role)
        {
            return await _roleStore.GetRoleIdAsync(role);
        }

        [HttpPost("GetRoleName")]
        public async Task<string> GetRoleNameAsync([FromBody] Role role)
        {
            return await _roleStore.GetRoleNameAsync(role);
        }

        [HttpPost("SetRoleName/{name}")]
        public async Task<string> SetRoleNameAsync([FromBody] Role role, string name)
        {
            await _roleStore.SetRoleNameAsync(role, name);
            await _roleStore.UpdateAsync(role);

            return role.Name;
        }

        [HttpPost("GetNormalizedRoleName")]
        public async Task<string> GetNormalizedRoleNameAsync([FromBody] Role role)
        {
            return await _roleStore.GetNormalizedRoleNameAsync(role);
        }

        [HttpPost("SetNormalizedRoleName/{name}")]
        public async Task<string> SetNormalizedRoleNameAsync([FromBody] Role role, string name)
        {
            await _roleStore.SetNormalizedRoleNameAsync(role, name);
            await _roleStore.UpdateAsync(role);

            return role.NormalizedName;
        }

        [HttpGet("FindById/{id}")]
        public async Task<Role> FindByIdAsync(string id) => await _roleStore.FindByIdAsync(id);

        [HttpGet("FindByName/{name}")]
        public async Task<Role> FindByNameAsync(string name) => await _roleStore.FindByNameAsync(name);
    }
}
