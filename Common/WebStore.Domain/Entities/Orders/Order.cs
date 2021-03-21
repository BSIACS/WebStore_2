using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Identity;

namespace WebStore.Domain.Entities.Orders
{
    public class Order : NamedEntity
    {
        [Required]
        public User User { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
