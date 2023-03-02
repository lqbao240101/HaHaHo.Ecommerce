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

        public async Task<Address> AddNewAddressAsync(NewAddressModel data)
        {
            var newAddress = new Address()
            {
                Street = data.Street,
                Ward = data.Ward,
                District= data.District,
                City = data.City,
                CustomerId = data.CustomerId,
            };

            await _context.Addresses.AddAsync(newAddress);
            await _context.SaveChangesAsync();
            return newAddress;
        }

        public async Task<Address> GetAddressDetail(string userId, int id)
        {
            var address = await _context.Addresses.FirstOrDefaultAsync(n => n.Id == id && n.CustomerId == userId);

            return address;           
        }

        public async Task<List<Address>> GetAddressesByUserIdAndRoleAsync(string userId)
        {
            var addresses = await _context.Addresses.ToListAsync();

            addresses = addresses.Where(n => n.CustomerId == userId).ToList();
            
            return addresses;
        }

        public async Task UpdateAddressAsync(string customerId,NewAddressModel data)
        {
            var address = await _context.Addresses.FirstOrDefaultAsync(n => n.Id == data.Id && n.CustomerId == customerId);

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
