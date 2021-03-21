using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities.Orders
{
    public class OrderItem : Entity
    {
        [Required]
        public Order Order { get; set; }

        [Required]
        public Product Product { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
