using Ecommerce.Data.Base;
using Ecommerce.Data.IService;
using Ecommerce.Models;
using HAHAHO.ShopHuongDuong.Data.ViewModels;

namespace Ecommerce.Data.Service
{
    public class ProductService : EntityBaseRepository<Product>, IProductService
    {
        public ProductService(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Product> AddNewProductAsync(CreateProductModel data)
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

        //public async Task<Product> GetProductByIdAsync(int id)
        //{
        //    var productDetails = await _context.Products
        //        .Include(c => c.Category)
        //        .FirstOrDefaultAsync(n => n.Id == id);

        //    return productDetails;
        //}
    }
}
