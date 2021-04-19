using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Employees;

namespace WebStore.Interfaces.Interfaces
{
    public interface IEmployeesDataService
    {
        IEnumerable<Employee> GetAll();

        Employee GetById(int id);

        int Create(Employee employee);

        void Update(Employee employee);

        bool Delete(int id);

        IEnumerable<Profession> GetProfessions();
    }
}
