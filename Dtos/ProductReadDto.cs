using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Dtos
{
    public class ProductReadDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductUnit { get; set; }
        public double ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductStatus { get; set; }
    }
}
