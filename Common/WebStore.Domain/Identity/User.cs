using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.Identity
{
    public class User : IdentityUser
    {
        public const string Administrator = "Administrator";

        public const string DeafultAdminPassword = "Qwerty12";

        public string Description { get; set; }
    }
}
