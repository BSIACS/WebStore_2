using Microsoft.AspNetCore.Http;
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
    }
}
