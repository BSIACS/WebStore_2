using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Domain.Models
{
    public class Employee
    {
        /// <summary>Идентификатор</summary>
        public int Id { get; set; }

        /// <summary>Имя</summary>
        public string Name { get; set; }

        /// <summary>Фамилия</summary>
        public string Surename { get; set; }

        /// <summary>Отчество</summary>
        public string Patronymic { get; set; }

        /// <summary>Пол</summary>
        public string Gender { get; set; }

        /// <summary>Возраст</summary>
        public int? Age { get; set; }

        /// <summary>Профессия</summary>
        public string Profession { get; set; }
    }
}
