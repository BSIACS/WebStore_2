using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = WebStore.Domain.Identity.Role.Administrator)]
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
