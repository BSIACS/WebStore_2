using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WebStore.Domain.Employees;
using WebStore.Interfaces.Interfaces;

namespace WebStore.Clients
{
    public class EmployeesClient : BaseClient, IEmployeesDataService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmployeesClient> _logger;

        public EmployeesClient(IConfiguration configuration, ILogger<EmployeesClient> logger) : base(configuration, "/api/EmployeesApi")
        {
            _configuration = configuration;
            _logger = logger;
        }

        public IEnumerable<Employee> GetAll()
        {
            return Get<IEnumerable<Employee>>(Address);
        }

        public IEnumerable<Profession> GetProfessions()
        {
            return Get<IEnumerable<Profession>>($"{Address}/GetProfessions");
        }

        public Employee GetById(int id)
        {
            return Get<Employee>($"{Address}/{id}");
        }


        public int Create(Employee employee)
        {
            return Post(Address, employee).Content.ReadAsAsync<int>().Result;
        }

        public void Update(Employee employee)
        {
            Put(Address, employee);
        }              

        public bool Delete(int id)
        {
            return Delete($"{Address}/{id}").IsSuccessStatusCode;
        }
    }
}
