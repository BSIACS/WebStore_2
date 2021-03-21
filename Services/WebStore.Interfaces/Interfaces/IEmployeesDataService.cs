using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Employees;

namespace WebStore.Interfaces.Interfaces
{
    public interface IEmployeesDataService
    {
        IList<Employee> GetAll();

        Employee GetById(int id);

        int Add(Employee employee);

        void Edit(Employee employee);

        bool Remove(int id);

        IEnumerable<Profession> GetProfessions();
    }
}
