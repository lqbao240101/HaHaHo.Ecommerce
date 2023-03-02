using Ecommerce.Data.Base;
using Ecommerce.Data.IService;
using Ecommerce.Models;

namespace Ecommerce.Data.Service
{
    public class CategoryService : EntityBaseRepository<Category>, ICategoryService
    {
        public CategoryService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
