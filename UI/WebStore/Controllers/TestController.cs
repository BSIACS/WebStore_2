using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Clients;

namespace WebStore.Controllers
{
    public class TestController : Controller
    {
        private readonly EmployeesClient _employeesClient;

        public TestController(EmployeesClient employeesClient)
        {
            _employeesClient = employeesClient;
        }

        public void Index()
        {
            var emps = _employeesClient.GetAll();
        }
    }
}
