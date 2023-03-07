using Ecommerce.Data.Base;
using Ecommerce.Data.IService;
using Ecommerce.Data.ViewModels;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Ecommerce.Data.Service
{
    public class AddressService : EntityBaseRepository<Address>, IAddressService
    {
        public AddressService(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddNewAddressAsync(string userId, NewAddressModel data)
        {
            var newAddress = new Address()
            {
                Street = data.Street,
                Ward = data.Ward,
                District= data.District,
                City = data.City,
                CustomerId = userId,
            };

            await _context.Addresses.AddAsync(newAddress);
            await _context.SaveChangesAsync();
        }

        public async Task<Address> GetAddressDetail(string userId, int id)
        {
            var address = await _context.Addresses.FirstOrDefaultAsync(n => n.Id == id && n.CustomerId == userId);

            return address;
        }

        public async Task<List<Address>> GetAddressesByUserId(string userId)
        {
            var addresses = await _context.Addresses.Where(n => n.CustomerId == userId).ToListAsync();
            
            return addresses;
        }

        public async Task UpdateAddressAsync(UpdateAddressModel data)
        {
            var address = await _context.Addresses.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (address != null)
            {
                address.Street = data.Street;
                address.Ward = data.Ward;
                address.District = data.District;
                address.City = data.City;
                address.CustomerId = data.CustomerId;
                await _context.SaveChangesAsync();
            }
        }
    }
}
