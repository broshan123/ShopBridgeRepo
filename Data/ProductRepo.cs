using Microsoft.EntityFrameworkCore;
using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Data
{
    public class ProductRepo : IProductRepo
    {
        private readonly ProductContext _productContext;

        public ProductRepo(ProductContext productContext)
        {
            _productContext = productContext;
        }
        public async Task AddProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException();
            }
            await _productContext.AddAsync(product);
            await _productContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productContext.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _productContext.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task RemoveProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            _productContext.Products.Remove(product);
            await _productContext.SaveChangesAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            _productContext.Products.Update(product);
            await _productContext.SaveChangesAsync();
        }
    }
}
