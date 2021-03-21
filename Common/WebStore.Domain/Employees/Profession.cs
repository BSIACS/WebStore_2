using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebStore.Domain.Employees
{
    public class Profession
    {
        /// <summary>Идентификатор</summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>Название профессии</summary>
        [Required]
        public string Name { get; set; }
    }
}
