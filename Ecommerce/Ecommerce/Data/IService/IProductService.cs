using Ecommerce.Data.Base;
using Ecommerce.Data.ViewModels;
using Ecommerce.Models;

namespace Ecommerce.Data.IService
{
    public interface IProductService : IEntityBaseRepository<Product>
    {
        Task AddNewProductAsync(NewProductModel data);

        Task UpdateProductAsync(NewProductModel data);
    }
}
