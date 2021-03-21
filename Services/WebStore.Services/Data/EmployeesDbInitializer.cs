using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Employees;
using WebStore.Employees.DAL.Context;

namespace WebStore.Services.Data
{
    public class EmployeesDbInitializer
    {
        private readonly EmployeesDb _employeesDb;
        private readonly ILogger<EmployeesDbInitializer> _logger;

        public EmployeesDbInitializer(ILogger<EmployeesDbInitializer> logger, EmployeesDb employeesDb)
        {
            _employeesDb = employeesDb;
            _logger = logger;
        }

        public void Initialize()
        {
            _logger.LogInformation("Старт инициализации БД сотрудников");

            var dataBase = _employeesDb.Database;

            if (dataBase.GetPendingMigrations().Any())
            {
                _logger.LogInformation("Есть неприменённые миграции БД сотрудников...");
                dataBase.Migrate();
                _logger.LogInformation("Миграции БД сотрудников выполнены...");
            }
            else
            {
                _logger.LogInformation("БД сотрудников в актуальном состоянии...");
            }

            try
            {
                InitializeDatabase();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Ошибка при инициализации таблицы сотрудников");

                throw;
            }
        }

        private void InitializeDatabase()
        {
            if (_employeesDb.Employees.Any())
                return;

            _logger.LogInformation("Старт инициализации БД сотрудников...");

            using (_employeesDb.Database.BeginTransaction())
            {
                _employeesDb.Professions.AddRange(new List<Profession>() {
                    new Profession { Id = 1, Name = "Генеральный директор"},
                    new Profession { Id = 2, Name = "Главный бухгалтер"},
                    new Profession { Id = 3, Name = "Бухгалтер"},
                    new Profession { Id = 4, Name = "Контент-менеджер"},
                    new Profession { Id = 5, Name = "Менеджер по заказам"},
                    new Profession { Id = 6, Name = "Кладовщик"},
                });

                _employeesDb.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Professions] ON");
                _employeesDb.SaveChanges();
                _employeesDb.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Professions] OFF");

                _employeesDb.Database.CommitTransaction();
            }

            using (_employeesDb.Database.BeginTransaction())
            {
                _employeesDb.Employees.AddRange(new List<Employee>() {
                    new Employee { Name = "Евграф", Surename = "Антонов", Patronymic = "Дартвэйдерович", Gender = "Мужской", Age = 49, ProfessionId = 1 },
                    new Employee { Name = "Наина", Surename = "Горыныч", Patronymic = "Киевна", Gender = "Женский", Age = 54, ProfessionId = 2 },
                    new Employee { Name = "Руслана", Surename = "Черных", Patronymic = "Котофеевна", Gender = "Женский", Age = 45, ProfessionId = 3 },
                    new Employee { Name = "Петр", Surename = "Петров", Patronymic = "Петрович", Gender = "Мужской", Age = 30, ProfessionId = 4 },
                    new Employee { Name = "Алексей", Surename = "Алексеев", Patronymic = "Алексеевич", Gender = "Мужской", Age = 31, ProfessionId = 4 },
                    new Employee { Name = "Ольга", Surename = "Ольгеева", Patronymic = "Ольговна", Gender = "Женский", Age = 32, ProfessionId = 4 },
                    new Employee { Name = "Ян", Surename = "Антонов", Patronymic = "Инокентиевич", Gender = "Мужской", Age = 36, ProfessionId = 5 },
                    new Employee { Name = "Иван", Surename = "Иванов", Patronymic = "Иванович", Gender = "Мужской", Age = 24, ProfessionId = 5 },
                    new Employee { Name = "Сидор", Surename = "Сидоров", Patronymic = "Сидорович", Gender = "Мужской", Age = 25, ProfessionId = 5 },
                    new Employee { Name = "Анна", Surename = "Плюшкина", Patronymic = "Генадиевна", Gender = "Женский", Age = 26, ProfessionId = 5 },
                    new Employee { Name = "Варвара", Surename = "Загребнюк", Patronymic = "Ульяновна", Gender = "Женский", Age = 27, ProfessionId = 6 },
                    new Employee { Name = "Баал", Surename = "Длиннорукий", Patronymic = "Степанович", Gender = "Мужской", Age = 49, ProfessionId = 6 },

                });

                _employeesDb.SaveChanges();

                _employeesDb.Database.CommitTransaction();

                _logger.LogInformation("БД сотрудников инициализированна...");
            }


        }
    }
}
