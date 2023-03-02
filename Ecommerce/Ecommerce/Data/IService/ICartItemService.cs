using Ecommerce.Data.Base;
using Ecommerce.Data.ViewModels;
using Ecommerce.Models;

namespace Ecommerce.Data.IService
{
    public interface ICartItemService
    {
        Task<CartItem> Create(NewCartItemModel data);

        //Task Update();

        //Task Delete();
    }
}
