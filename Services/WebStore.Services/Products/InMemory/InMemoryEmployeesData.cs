using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Employees;
using WebStore.Interfaces.Interfaces;
using WebStore.Services.Data;

namespace WebStore.Services.Products.InMemory
{
    public class InMemoryEmployeesData : IEmployeesDataService
    {
        readonly IList<Employee> _employees;

        public InMemoryEmployeesData()
        {
            _employees = EmployeesInfoProvider.Employees;
        }

        public int Add(Employee employee)
        {
            employee.Id = _employees.Max(x => x.Id) + 1;
            _employees.Add(employee);

            return employee.Id;
        }

        public void Edit(Employee employee)
        {
            Employee emp = _employees.FirstOrDefault(e => e.Id == employee.Id);

            emp.Id = employee.Id;
            emp.Name = employee.Name;
            emp.Surename = employee.Surename;
            emp.Patronymic = employee.Patronymic;
            emp.Age = employee.Age;
            emp.Gender = employee.Gender;
            emp.Profession = employee.Profession;
        }

        public IList<Employee> GetAll() => _employees;

        public Employee GetById(int id)
        {
            return _employees.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Profession> GetProfessions()
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            Employee emp = GetById(id);

            if (emp is null)
                return false;

            _employees.Remove(emp);

            return true;
        }
    }
}
