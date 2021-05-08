using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Clients;

namespace WebStore.Controllers
{
    public class TestController : Controller
    {
        private readonly ILogger _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        public void Index()
        {
            using (_logger.BeginScope<string>("Test area (begin scope)___________")) {
                _logger.LogInformation("Test info logs");
                _logger.LogWarning("Test warning logs");
            }
            
            //var emps = _employeesClient.GetAll();
        }
    }
}
