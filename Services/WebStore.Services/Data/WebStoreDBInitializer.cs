using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Identity;

namespace WebStore.Services.Data
{
    public class WebStoreDbInitializer
    {
        private readonly WebStoreDB _webStoreDB;
        private readonly ILogger<WebStoreDbInitializer> _logger;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;


        public WebStoreDbInitializer(
            WebStoreDB webStoreDB,
            ILogger<WebStoreDbInitializer> logger,
            UserManager<User> userManager,
            RoleManager<Role> roleManager
            )
        {
            _webStoreDB = webStoreDB;
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Initialize()
        {
            _logger.LogInformation("Старт инициализации БД");

            var dataBase = _webStoreDB.Database;

            if (dataBase.GetPendingMigrations().Any())
            {
                _logger.LogInformation("Есть неприменённые миграции...");
                dataBase.Migrate();
                _logger.LogInformation("Миграции БД выполнены...");
            }
            else
            {
                _logger.LogInformation("БД в актуальном состоянии...");
            }

            try
            {
                InitializeProducts();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Ошибка при инициализации таблицы товаров");
                throw;
            }

            try
            {
                InitializeIdentityAsync().Wait();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Ошибка при инициализации БД системы Identity");
                throw;
            }
            finally
            {
                _logger.LogError(null, "Test, test, test!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }


        }

        private void InitializeProducts()
        {
            if (_webStoreDB.Products.Any())
            {
                return;
            }

            using (_webStoreDB.Database.BeginTransaction())
            {

                _webStoreDB.Sections.AddRange(TestData.Sections);

                _webStoreDB.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] ON");
                _webStoreDB.SaveChanges();
                _webStoreDB.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] OFF");


                _webStoreDB.Database.CommitTransaction();
            }

            using (_webStoreDB.Database.BeginTransaction())
            {

                _webStoreDB.Brands.AddRange(TestData.Brands);

                _webStoreDB.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] ON");
                _webStoreDB.SaveChanges();
                _webStoreDB.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] OFF");


                _webStoreDB.Database.CommitTransaction();
            }

            using (_webStoreDB.Database.BeginTransaction())
            {

                _webStoreDB.Products.AddRange(TestData.Products);

                _webStoreDB.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] ON");
                _webStoreDB.SaveChanges();
                _webStoreDB.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] OFF");


                _webStoreDB.Database.CommitTransaction();
            }
        }

        public async Task InitializeIdentityAsync()
        {

            async Task EnsureRoleCreated(string name)
            {
                if (!await _roleManager.RoleExistsAsync(name))
                    await _roleManager.CreateAsync(new Role { Name = name });
            }

            await EnsureRoleCreated(Role.Administrator);
            await EnsureRoleCreated(Role.User);

            if (await _userManager.FindByNameAsync(User.Administrator) is null)
            {
                User adminUser = new User { UserName = User.Administrator };

                var userCreationResult = await _userManager.CreateAsync(adminUser, User.DeafultAdminPassword);

                if (userCreationResult.Succeeded)
                {
                    var addToRoleResult = await _userManager.AddToRoleAsync(adminUser, Role.Administrator);

                    if (!addToRoleResult.Succeeded)
                    {
                        var errors = addToRoleResult.Errors.Select(e => e.Description);
                        throw new InvalidOperationException($"Ошибка при добавлении роли пользователю 'Администратор'. {string.Join(',', errors)}");
                    }
                }
                else
                {
                    var errors = userCreationResult.Errors.Select(e => e.Description);
                    throw new InvalidOperationException($"Ошибка при создании учетной записи пользователя 'Администратор'. {string.Join(',', errors)}");
                }
            }

        }
    }
}
