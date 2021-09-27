using ShopBridge.Data;
using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridgeTestProject
{
    public class ProductRepoFack : IProductRepo
    {
        private readonly List<Product> _productList;
        public ProductRepoFack()
        {
            _productList = new List<Product>()
            {
                new Product() { ProductId = 1, ProductName = "Nokia Phone", ProductDescription = "Nokia Phone", ProductPrice = 100, ProductQuantity = 8, ProductUnit = "Numeber" },
                new Product() { ProductId = 2, ProductName = "I Phone", ProductDescription = "I Phone", ProductPrice = 123, ProductQuantity = 18, ProductUnit = "Numeber" },
                new Product() { ProductId = 3, ProductName = "Jio Phone", ProductDescription = "Jio Phone", ProductPrice = 124, ProductQuantity = 80, ProductUnit = "Numeber" }
            };
        }
    
        
        public async Task AddProduct(Product product)
        {
            product.ProductId = _productList.Count+1;
            _productList.Add(product);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return  _productList;
        }

        public async Task<Product> GetProductById(int id)
        {
            return  _productList.FirstOrDefault(p => p.ProductId == id);
        }

        public async Task RemoveProduct(Product product)
        {
            var existing = _productList.FirstOrDefault(a => a.ProductId == product.ProductId);
            _productList.Remove(existing);
        }

        public Task UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
