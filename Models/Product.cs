using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ProductName { get; set; }

        [MaxLength(2000)]
        public string ProductDescription { get; set; }

        [MaxLength(10)]
        public string ProductUnit { get; set; }

        public double ProductPrice { get; set; }

        public int ProductQuantity { get; set; }
    }
}
