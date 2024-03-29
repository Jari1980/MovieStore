﻿using System.ComponentModel.DataAnnotations;

namespace Movie_Store.Models
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;
        public virtual ICollection<OrderRows> OrderRows { get; set; }


        public int CustomerId { get; set; }
        
        public virtual Customers Customer { get; set; }
    }
}
