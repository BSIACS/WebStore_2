using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Employees;
using WebStore.Interfaces.Interfaces;

namespace WebStore.Clients.Employees
{
    class EmployeesClient : BaseClient, IEmployeesDataService
    {
        private readonly ILogger<EmployeesClient> _logger;

        public EmployeesClient(IConfiguration configuration, ILogger<EmployeesClient> logger) : base(configuration, "api/Employees")
        {
            _logger = logger;
        }

        public IList<Employee> GetAll()
        {
            throw new NotImplementedException();
        }

        public Employee GetById(int id)
        {
            throw new NotImplementedException();
        }


        public int Add(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void Edit(Employee employee)
        {
            throw new NotImplementedException();
        } 
        
        public IEnumerable<Profession> GetProfessions()
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
