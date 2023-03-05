using AutoMapper;
using Ecommerce.Data.ViewModels;
using Ecommerce.Models;

namespace Ecommerce.Helper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Order, OrderView>();
        }
    }
}
