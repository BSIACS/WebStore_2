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
using WebStore.Domain.DTO.Identity;
using WebStore.Domain.Identity;

namespace WebStore.ServiceHosting.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersApiController : ControllerBase
    {
        private readonly UserStore<User, Role, WebStoreDB> _userStore;

        public UsersApiController(WebStoreDB dB)
        {
            _userStore = new UserStore<User, Role, WebStoreDB>(dB);
        }

        [HttpGet("AllUsers")]
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userStore.Users.ToArrayAsync();
        }

        #region Users

        [HttpPost("UserId")]
        public async Task<string> GetUserIdAsync([FromBody] User user) => await _userStore.GetUserIdAsync(user);

        [HttpPost("UserName")]
        public async Task<string> GetUserNameAsync([FromBody] User user) => await _userStore.GetUserNameAsync(user);

        [HttpPost("UserName/{name}")]
        public async Task<string> SetUserName([FromBody] User user, string name) {
            await _userStore.SetUserNameAsync(user, name);
            await _userStore.UpdateAsync(user);

            return user.UserName;
        }

        [HttpPost("NormalizedName")]
        public async Task<string> GetNormalizedUserNameAsync([FromBody] User user) => await _userStore.GetNormalizedUserNameAsync(user);

        [HttpPost("NormalizedName/{name}")]
        public async Task<string> SetNormalizedUserName([FromBody] User user, string name)
        {
            await _userStore.SetNormalizedUserNameAsync(user, name);
            await _userStore.UpdateAsync(user);

            return user.NormalizedUserName;
        }

        [HttpPost("User")]
        public async Task<bool> CreateAsync([FromBody] User user) {
            IdentityResult creation_result = await _userStore.CreateAsync(user);

            return creation_result.Succeeded;
        }

        [HttpPut("User")]
        public async Task<bool> UpdateAsync([FromBody] User user)
        {
            IdentityResult update_result = await _userStore.UpdateAsync(user);

            return update_result.Succeeded;
        }

        [HttpDelete("User/Delete")]
        public async Task<bool> DeleteAsync([FromBody] User user)
        {
            IdentityResult delete_result = await _userStore.DeleteAsync(user);

            return delete_result.Succeeded;
        }

        [HttpGet("User/Find/{id}")]
        public async Task<User> FindByIdAsync(string id)
        {
            User user = await _userStore.FindByIdAsync(id);

            return user;
        }

        [HttpGet("User/Normal/{name}")]
        public async Task<User> FindByNameAsync(string name)
        {
            User user = await _userStore.FindByNameAsync(name);

            return user;
        }

        [HttpPost("Role/{role}")]
        public async Task AddToRoleAsync([FromBody] User user, string role, [FromServices] WebStoreDB dB)
        {
            await _userStore.AddToRoleAsync(user, role);
            await dB.SaveChangesAsync();
        }

        [HttpPost("Role/Delete/{role}")]
        public async Task RemoveFromRoleAsync([FromBody] User user, string role, [FromServices] WebStoreDB dB)
        {
            await _userStore.RemoveFromRoleAsync(user, role);
            await dB.SaveChangesAsync();
        }

        [HttpPost("Roles")]
        public async Task<IList<string>> GetRolesAsync([FromBody] User user)
        {
            return await _userStore.GetRolesAsync(user);
        }

        [HttpPost("Role/{role}")]
        public async Task<bool> IsInRoleAsync([FromBody] User user, string role)
        {
            return await _userStore.IsInRoleAsync(user, role);
        }

        [HttpGet("UsersInRole/{role}")]
        public async Task<IList<User>> GetUsersInRoleAsync(string role)
        {
            return await _userStore.GetUsersInRoleAsync(role);
        }

        [HttpPost("GetPasswordHash")]
        public async Task<string> GetPasswordHashAsync([FromBody] User user)
        {
            return await _userStore.GetPasswordHashAsync(user);
        }

        [HttpPost("SetPasswordHash")]
        public async Task<string> SetPasswordHashAsync([FromBody] User user, PasswordHashDTO hash)
        {
            await _userStore.SetPasswordHashAsync(user, hash.Hash);
            await _userStore.UpdateAsync(hash.User);

            return hash.User.PasswordHash;
        }

        [HttpPost("HasPassword")]
        public async Task<bool> HasPasswordAsync([FromBody] User user)
        {
            return await _userStore.HasPasswordAsync(user);
        }

        #endregion
    }
}
