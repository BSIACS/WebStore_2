using Microsoft.EntityFrameworkCore;
using System;
using WebStore.Domain.Employees;

namespace WebStore.Employees.DAL.Context
{
    public class EmployeesDb : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Profession> Professions { get; set; }

        public EmployeesDb(DbContextOptions<EmployeesDb> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
