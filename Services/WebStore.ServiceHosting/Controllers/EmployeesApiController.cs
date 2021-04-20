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
    /// <summary>
    /// API управления базой данных сотрудников
    /// </summary>
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

        /// <summary>
        /// Получение списка всех сотрудников
        /// </summary>
        /// <returns>список сотрудников</returns>
        [HttpGet]
        public IEnumerable<Employee> GetAll()
        {
            var result = _employeesData.GetAll();

            return result;
        }

        /// <summary>
        /// Получение списка всех профессий
        /// </summary>
        /// <returns>список профессий</returns>
        [HttpGet]
        [Route("GetProfessions")]
        public IEnumerable<Profession> GetProfessions() => _employeesData.GetProfessions();

        /// <summary>
        /// Получение сотрудника по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сотрудника</param>
        /// <returns>Сотрудник</returns>
        [HttpGet("{id}")]
        public Employee GetById(int id) => _employeesData.GetById(id);

        /// <summary>
        /// Добавление нового сотрудника
        /// </summary>
        /// <param name="employee">Добавляемый сотрудник</param>
        /// <returns>Идентификатор сотрудника</returns>
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

        /// <summary>
        /// Редактирование сотрудника
        /// </summary>
        /// <param name="employee">Редактируемый сотрудник</param>
        [HttpPut]
        public void Update(Employee employee) => _employeesData.Update(employee);

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="id">Идентификатор удаляемого сотрудника</param>
        /// <returns>True - сотрудник удалён успешно, false - ошибка при удалении сотрудника</returns>
        [HttpDelete("{id}")]
        public bool Delete(int id) => _employeesData.Delete(id);
          
    }
}
