using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
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
    public class UsersApiController : ControllerBase
    {
        private readonly UserStore<User, Role, WebStoreDB> _userStore;

        public UsersApiController(WebStoreDB dB)
        {
            _userStore = new UserStore<User, Role, WebStoreDB>(dB);
        }
    }
}
