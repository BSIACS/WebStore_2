using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Employees;
using WebStore.Interfaces.Services;
using WebStore.Employees.DAL.Context;

namespace WebStore.Services.Products.InSqlDataBase
{
    public class InSqlDbEmployeesData : IEmployeesDataService
    {
        private readonly EmployeesDb _db;

        public InSqlDbEmployeesData(EmployeesDb db)
        {
            _db = db;
        }

        public int Create(Employee employee)
        {
            _db.Add(employee);
            _db.SaveChanges();

            return employee != null ? employee.Id : 0;
        }

        public void Update(Employee employee)
        {
            _db.Entry(employee).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public IEnumerable<Employee> GetAll()
        {
            IQueryable<Employee> employees = _db.Employees.Include(emp => emp.Profession);
            var er = employees.ToList();
            return employees.ToList();
        }

        public Employee GetById(int id)
        {
            return _db.Employees.Include(e => e.Profession).FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Profession> GetProfessions()
        {
            return _db.Professions;
        }

        public bool Delete(int id)
        {
            Employee employee = _db.Employees.FirstOrDefault(e => e.Id == id);

            if (employee != null)
            {
                _db.Employees.Remove(employee);
                _db.SaveChanges();
                return true;
            }
            else
                return false;
        }
    }
}
