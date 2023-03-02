using Ecommerce.Data.Base;
using Ecommerce.Models;
using HAHAHO.ShopHuongDuong.Data.ViewModels;

namespace Ecommerce.Data.IService
{
    public interface IProductService : IEntityBaseRepository<Product>
    {
        //Task<Product> GetProductByIdAsync(int id);
        Task<Product> AddNewProductAsync(CreateProductModel data);
    }
}
