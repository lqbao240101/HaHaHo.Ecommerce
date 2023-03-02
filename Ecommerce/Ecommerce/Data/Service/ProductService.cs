using Ecommerce.Data.Base;
using Ecommerce.Data.IService;
using Ecommerce.Data.ViewModels;
using Ecommerce.Models;
using HAHAHO.ShopHuongDuong.Data.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data.Service
{
    public class ProductService : EntityBaseRepository<Product>, IProductService
    {
        public ProductService(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Product> AddNewProductAsync(NewProductModel data)
        {
            var newProduct = new Product()
            {
                ProductName = data.ProductName,
                Description = data.Description,
                Price = data.Price,
                PercentSale = data.PercentSale,
                ProductImg = data.ProductImg,
                CategoryId = data.CategoryId,
                Quantity = data.Quantity
            };
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
            return newProduct;
        }

        public async Task UpdateProductAsync(NewProductModel data)
        {
            var product = await _context.Products.FirstOrDefaultAsync(n => n.Id == data.Id);
        
            if(product != null) 
            {
                product.ProductName = data.ProductName;
                product.Description = data.Description;
                product.Price = data.Price;
                product.PercentSale = data.PercentSale;
                product.ProductImg = data.ProductImg;
                product.CategoryId = data.CategoryId;
                product.Quantity = data.Quantity;
                await _context.SaveChangesAsync();
            }
        }

        //public async Task<Product> GetProductByIdAsync(int id)
        //{
        //    var productDetails = await _context.Products
        //        .Include(c => c.Category)
        //        .FirstOrDefaultAsync(n => n.Id == id);

        //    return productDetails;
        //}
    }
}
