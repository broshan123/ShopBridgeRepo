using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Dtos
{
    public class ProductAddDto
    {
        [Required]
        [MaxLength(50)]
        public string ProductName { get; set; }

        [MaxLength(2000)]
        public string ProductDescription { get; set; }

        [MaxLength(20)]
        public string ProductUnit { get; set; }

        public double ProductPrice { get; set; }

        public int ProductQuantity { get; set; }
    }
}
