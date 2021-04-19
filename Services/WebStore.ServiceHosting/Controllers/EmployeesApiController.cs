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
using WebStore.Interfaces.Services;
using WebStore.Services.Products.InSqlDataBase;

namespace WebStore.ServiceHosting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeesDataService
    {
        private readonly IEmployeesDataService _employeesData;
        private readonly ILogger<EmployeesApiController> _logger;

        public EmployeesApiController(IEmployeesDataService employeesData, ILogger<EmployeesApiController> logger)
        {
            _employeesData = employeesData;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Employee> GetAll()
        {
            var result = _employeesData.GetAll();

            return result;
        }

        [HttpGet]
        [Route("GetProfessions")]
        public IEnumerable<Profession> GetProfessions() => _employeesData.GetProfessions();

        [HttpGet("{id}")]
        public Employee GetById(int id) => _employeesData.GetById(id);

        [HttpPost]
        public int Create(Employee employee)
        {
            if (!ModelState.IsValid) {
                _logger.LogWarning($"Ошибка модели данных при добавлении нового сотрудника " +
                    $"{employee.Name} {employee.Surename} {employee.Patronymic}");

                return 0;
            }

            _logger.LogInformation($"Добавление сотрудника {employee.Name} {employee.Surename} {employee.Patronymic}");

            int empID = _employeesData.Create(employee);

            if(empID != 0)
                _logger.LogInformation($"Cотрудника [id:{empID}] {employee.Name} {employee.Surename} {employee.Patronymic}" +
                    $"добавлен успешно");
            else
                _logger.LogInformation($"Ошибка при добавлении сотрудника {employee.Name} {employee.Surename} {employee.Patronymic}");

            return empID;
        }

        [HttpPut]
        public void Update(Employee employee) => _employeesData.Update(employee);

        [HttpDelete("{id}")]
        public bool Delete(int id) => _employeesData.Delete(id);
          
    }
}
