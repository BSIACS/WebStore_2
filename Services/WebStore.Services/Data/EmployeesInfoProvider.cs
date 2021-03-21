using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Employees;

namespace WebStore.Services.Data
{
    public static class EmployeesInfoProvider
    {
        private static IList<Employee> employees = new List<Employee>() {
            new Employee() { Id = 1, Name = "Денис", Surename = "Огурцов", Patronymic = "Сергеевич", Gender = "Мужской", Age = 38},
            new Employee() { Id = 2, Name = "Ольга", Surename = "Самохвалова", Patronymic = "Викторовна", Gender = "Женский", Age = 32},
            new Employee() { Id = 3, Name = "Виктор", Surename = "Быков", Patronymic = "Николаевич", Gender = "Мужской", Age = 55},
            new Employee() { Id = 4, Name = "Олег", Surename = "Калинин", Patronymic = "Викторович", Gender = "Мужской", Age = 38},
            new Employee() { Id = 5, Name = "Юрий", Surename = "Корюнин", Patronymic = "Анатольевич", Gender = "Мужской", Age = 46},
            new Employee() { Id = 6, Name = "Анна", Surename = "Огурцова", Patronymic = "Александровна", Gender = "Женский", Age = 35},
        };

        public static IList<Employee> Employees
        {
            get => employees;
            set => employees = value;
        }
    }
}
