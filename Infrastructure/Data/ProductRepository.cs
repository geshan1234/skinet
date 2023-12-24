using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(e => e.ProductType)
                .Include(e => e.ProductBrand)
                .FirstOrDefaultAsync(e => e.Id == id);
            if(product is null) 
            {
                return null;
            }

            return product;
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Products
                .Include(e => e.ProductType)
                .Include(e => e.ProductBrand)
                .ToListAsync();
        }
    }
}