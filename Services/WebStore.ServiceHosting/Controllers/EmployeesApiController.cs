using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Employees;
using WebStore.Employees.DAL.Context;
using WebStore.Interfaces.Interfaces;
using WebStore.Services.Products.InSqlDataBase;

namespace WebStore.ServiceHosting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeesDataService
    {
        private readonly InSqlDbEmployeesData _employeesData;
        private readonly ILogger<EmployeesApiController> _logger;

        public EmployeesApiController([FromServices]InSqlDbEmployeesData employeesData, ILogger<EmployeesApiController> logger)
        {
            _employeesData = employeesData;
            _logger = logger;
        }

        [HttpGet]
        public IList<Employee> GetAll() => _employeesData.GetAll();

        [HttpGet("{id}")]
        public Employee GetById(int id) => _employeesData.GetById(id);

        [HttpPost]
        public int Add(Employee employee)
        {
            if (!ModelState.IsValid) {
                _logger.LogWarning($"Ошибка модели данных при добавлении нового сотрудника " +
                    $"{employee.Name} {employee.Surename} {employee.Patronymic}");

                return 0;
            }

            _logger.LogInformation($"Добавление сотрудника {employee.Name} {employee.Surename} {employee.Patronymic}");

            int empID = _employeesData.Add(employee);

            if(empID != 0)
                _logger.LogInformation($"Cотрудника [id:{empID}] {employee.Name} {employee.Surename} {employee.Patronymic}" +
                    $"добавлен успешно");
            else
                _logger.LogInformation($"Ошибка при добавлении сотрудника {employee.Name} {employee.Surename} {employee.Patronymic}");

            return empID;
        }

        [HttpPut]
        public void Edit(Employee employee) => _employeesData.Edit(employee);

        [HttpDelete]
        public bool Remove(int id) => _employeesData.Remove(id);

        public IEnumerable<Profession> GetProfessions() => _employeesData.GetProfessions();        
    }
}
