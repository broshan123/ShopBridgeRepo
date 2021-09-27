using Microsoft.EntityFrameworkCore;
using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Data
{
    public interface IProductContext
    {
        DbSet<Product> Products { get; set; }
      
    }
    public class ProductContext : DbContext, IProductContext
    {
        public ProductContext(DbContextOptions<ProductContext> opt):base(opt)
        {

        }
        public DbSet<Product> Products { get; set; }
    }
}
