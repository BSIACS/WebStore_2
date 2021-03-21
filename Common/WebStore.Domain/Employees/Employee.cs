using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebStore.Domain.Employees
{
    public class Employee
    {
        /// <summary>Идентификатор</summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        /// <summary>Идентификатор профессии</summary>
        public int ProfessionId { get; set; }

        /// <summary>Профессия</summary>
        [ForeignKey(nameof(ProfessionId))]
        public Profession Profession { get; set; }
    }
}
