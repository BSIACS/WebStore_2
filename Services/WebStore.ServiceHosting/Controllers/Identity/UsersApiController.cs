using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        //Почему где-то dB.SaveChangesAsync(), а где-то _userStore.UpdateAsync(ex:user)

        #region Users

        [HttpPost("UserId")]
        public async Task<string> GetUserIdAsync([FromBody] User user) => await _userStore.GetUserIdAsync(user);

        [HttpPost("UserName")]
        public async Task<string> GetUserNameAsync([FromBody] User user) => await _userStore.GetUserNameAsync(user);

        [HttpPost("UserName/{name}")]
        public async Task<string> SetUserName([FromBody] User user, string name)
        {
            await _userStore.SetUserNameAsync(user, name);
            await _userStore.UpdateAsync(user);

            return user.UserName;
        }

        [HttpPost("NormalUserName")]
        public async Task<string> GetNormalizedUserNameAsync([FromBody] User user) => await _userStore.GetNormalizedUserNameAsync(user);

        [HttpPost("NormalUserName/{name}")]
        public async Task<string> SetNormalizedUserName([FromBody] User user, string name)
        {
            await _userStore.SetNormalizedUserNameAsync(user, name);
            await _userStore.UpdateAsync(user);

            return user.NormalizedUserName;
        }

        [HttpPost("User")]
        public async Task<bool> CreateAsync([FromBody] User user)
        {
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

        [HttpPost("InRole/{role}")]
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
        public async Task<string> SetPasswordHashAsync([FromBody] PasswordHashDTO hash)
        {
            await _userStore.SetPasswordHashAsync(hash.User, hash.Hash);
            await _userStore.UpdateAsync(hash.User);

            return hash.User.PasswordHash;
        }

        [HttpPost("HasPassword")]
        public async Task<bool> HasPasswordAsync([FromBody] User user)
        {
            return await _userStore.HasPasswordAsync(user);
        }

        #endregion

        #region Calims

        [HttpPost("GetClaims")]
        public async Task<IList<Claim>> GetClaimsAsync([FromBody] User user) {
            return await _userStore.GetClaimsAsync(user);
        }

        [HttpPost("AddClaim")]
        public async Task AddClaimsAsync([FromBody] AddClaimDTO claimInfo, [FromServices] WebStoreDB dB)
        {
            await _userStore.AddClaimsAsync(claimInfo.User, claimInfo.Claims);
            await dB.SaveChangesAsync();
        }

        [HttpPost("ReplaceClaim")]
        public async Task ReplaceClaimAsync([FromBody] ReplaceClaimDTO claimInfo, [FromServices] WebStoreDB dB)
        {
            await _userStore.ReplaceClaimAsync(claimInfo.User, claimInfo.Claim, claimInfo.NewClaim);
            await dB.SaveChangesAsync();
        }

        [HttpPost("RemoveClaim")]
        public async Task RemoveClaimAsync([FromBody] RemoveClaimDTO claimInfo, [FromServices] WebStoreDB dB)
        {
            await _userStore.RemoveClaimsAsync(claimInfo.User, claimInfo.Claims);
            await dB.SaveChangesAsync();
        }

        [HttpPost("GetUsersForClaim")]
        public async Task<IList<User>> GetUsersForClaimAsync([FromBody] Claim claim)
        {
            return await _userStore.GetUsersForClaimAsync(claim);
        }

        #endregion

        #region TwoFactor

        [HttpPost("GetTwoFactorEnabled")]
        public async Task<bool> GetTwoFactorEnabledAsync([FromBody] User user) {
            return await _userStore.GetTwoFactorEnabledAsync(user);
        }

        [HttpPost("SetTwoFactorEnabled")]
        public async Task<bool> SetTwoFactorEnabledAsync([FromBody] User user, bool enable)
        {
            await _userStore.SetTwoFactorEnabledAsync(user, enable);
            await _userStore.UpdateAsync(user);

            return user.TwoFactorEnabled;
        }

        #endregion

        #region Email/Phone

        [HttpPost("GetEmail")]
        public async Task<string> GetEmailAsync([FromBody] User user) {
            return await _userStore.GetEmailAsync(user);
        }

        [HttpPost("SetEmail/{email}")]
        public async Task<string> SetEmailAsync([FromBody] User user, string email)
        {
            await _userStore.SetEmailAsync(user, email);
            await _userStore.UpdateAsync(user);

            return user.Email;
        }


        [HttpPost("GetEmailConfirmed")]
        public async Task<bool> GetEmailConfirmedAsync([FromBody] User user)
        {
            return await _userStore.GetEmailConfirmedAsync(user);
        }

        [HttpPost("SetEmailConfirmed/{enable}")]
        public async Task<bool> SetEmailAsync([FromBody] User user, bool enable)
        {
            await _userStore.SetEmailConfirmedAsync(user, enable);
            await _userStore.UpdateAsync(user);

            return user.EmailConfirmed;
        }

        [HttpPost("UserFindByEmail/{email}")]
        public async Task<User> FindByEmailAsync(string email)
        {
            
            return await _userStore.FindByEmailAsync(email);
        }

        [HttpPost("GetNormalizedEmail")]
        public async Task<string> GetNormalizedEmailAsync([FromBody] User user)
        {
            return await _userStore.GetNormalizedEmailAsync(user);
        }

        [HttpPost("SetNormalizedEmail/{email?}")]
        public async Task<string> SetNormalizedEmailAsync([FromBody] User user, string email)
        {
            await _userStore.SetNormalizedEmailAsync(user, email);
            await _userStore.UpdateAsync(user);

            return user.NormalizedEmail;
        }

        [HttpPost("GetPhoneNumber")]
        public async Task<string> GetPhoneNumberAsync([FromBody] User user)
        {
            return await _userStore.GetPhoneNumberAsync(user);
        }

        [HttpPost("SetPhoneNumber/{phone}")]
        public async Task<string> SetPhoneNumberAsync([FromBody] User user, string phone)
        {
            await _userStore.SetPhoneNumberAsync(user, phone);
            await _userStore.UpdateAsync(user);

            return user.PhoneNumber;
        }


        [HttpPost("GetPhoneNumberConfirmed")]
        public async Task<bool> GetPhoneNumberConfirmedAsync([FromBody] User user)
        {
            return await _userStore.GetPhoneNumberConfirmedAsync(user);
        }

        [HttpPost("SetPhoneNumberConfirmed/{enable}")]
        public async Task<bool> SetPhoneNumberConfirmedAsync([FromBody] User user, bool enable)
        {
            await _userStore.SetPhoneNumberConfirmedAsync(user, enable);
            await _userStore.UpdateAsync(user);

            return user.PhoneNumberConfirmed;
        }

        #endregion

        #region Login/Logout

        [HttpPost("AddLogin")]
        public async Task AddLoginAsync([FromBody] AddLoginDTO login, [FromServices] WebStoreDB dB)
        {
            await _userStore.AddLoginAsync(login.User, login.UserLoginInfo);
            await dB.SaveChangesAsync();
        }

        [HttpPost("RemoveLogin/{loginProvider}/{providerKey}")]
        public async Task<bool> RemoveLoginAsync([FromBody] User user, string loginProvider, string providerKey, [FromServices] WebStoreDB dB)
        {
            await _userStore.RemoveLoginAsync(user, loginProvider, providerKey);
            await dB.SaveChangesAsync();

            return user.PhoneNumberConfirmed;
        }

        [HttpPost("GetLogins")]
        public async Task<IList<UserLoginInfo>> GetLoginsAsync([FromBody] User user){
            return await _userStore.GetLoginsAsync(user);
        }

        [HttpPost("User/FindByLogin/{loginProvider}/{providerKey}")]
        public async Task<User> RemoveLoginAsync(string loginProvider, string providerKey)
        {
            return await _userStore.FindByLoginAsync(loginProvider, providerKey);            
        }

        [HttpPost("GetLockoutEndDate")]
        public async Task<DateTimeOffset?> GetLockoutEndDateAsync([FromBody] User user)
        {
            return await _userStore.GetLockoutEndDateAsync(user);
        }

        [HttpPost("SetLockoutEndDate")]
        public async Task<DateTimeOffset?> SetLockoutEndDateAsync([FromBody] SetLockoutDTO lockoutInfo)
        {
            await _userStore.SetLockoutEndDateAsync(lockoutInfo.User, lockoutInfo.LockoutEnd);
            await _userStore.UpdateAsync(lockoutInfo.User);

            return lockoutInfo.User.LockoutEnd;
        }

        [HttpPost("IncrementAccessFailedCount")]
        public async Task<int> IncrementAccessFailedCountAsync([FromBody] User user)
        {
            await _userStore.IncrementAccessFailedCountAsync(user);
            await _userStore.UpdateAsync(user);

            return user.AccessFailedCount;
        }

        [HttpPost("ResetAccessFailedCount")]
        public async Task<int> ResetAccessFailedCountAsync([FromBody] User user)
        {
            await _userStore.ResetAccessFailedCountAsync(user);
            await _userStore.UpdateAsync(user);

            return user.AccessFailedCount;
        }

        [HttpPost("GetAccessFailedCount")]
        public async Task<int> GetAccessFailedCountAsync([FromBody] User user)
        {
            await _userStore.GetAccessFailedCountAsync(user);
            await _userStore.UpdateAsync(user);

            return user.AccessFailedCount;
        }

        [HttpPost("GetLockoutEnabled")]
        public async Task<bool> GetLockoutEnabledAsync([FromBody] User user)
        {
            return await _userStore.GetLockoutEnabledAsync(user);
        }

        [HttpPost("SetLockoutEnabled/{enable}")]
        public async Task<bool> SetLockoutEnabledAsync([FromBody] User user, bool enable)
        {
            await _userStore.SetLockoutEnabledAsync(user, enable);
            await _userStore.UpdateAsync(user);

            return user.LockoutEnabled;
        }

        #endregion


       
    }
}
