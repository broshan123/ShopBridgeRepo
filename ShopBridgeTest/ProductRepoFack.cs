using Microsoft.EntityFrameworkCore;
using ShopBridge.Data;
using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridgeTest
{
    public class FakeProducttSet : FakeDbSet<Product>
    {
        public override Product Find(params object[] keyValues)
        {
            return this.SingleOrDefault(d => d.ProductId == (int)keyValues.Single());
        }
    }
    public class FakeProductContext : IProductContext
    {
        public DbSet<Product> Products { get;  set; }
    }

    public class ProductRepoFack : IProductRepo
    {
        private readonly FakeProductContext _fakeProductContext;
        public ProductRepoFack()
        {
            _fakeProductContext = new FakeProductContext
            {
                Products =
                {
                     new Product(){ProductId=1,ProductName="Nokia Phone", ProductDescription="Nokia Phone", ProductPrice=100, ProductQuantity=8, ProductUnit="Numeber"},
                     new Product(){ProductId=2,ProductName="I Phone", ProductDescription="I Phone", ProductPrice=123, ProductQuantity=18, ProductUnit="Numeber"},
                     new Product(){ProductId=3,ProductName="Jio Phone", ProductDescription="Jio Phone", ProductPrice=124, ProductQuantity=80, ProductUnit="Numeber"}
                }
            };
           
        }
        public async Task AddProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException();
            }
            await _fakeProductContext.Products.AddAsync(product);
            //await _fakeProductContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _fakeProductContext.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _fakeProductContext.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task RemoveProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            _fakeProductContext.Products.Remove(product);
        }

        public async Task UpdateProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            _fakeProductContext.Products.Update(product);
        }
    }


}
