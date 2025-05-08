using Microsoft.EntityFrameworkCore;
using TicketMaster.Data.DTOs;
using TicketMaster.Data.Services.Interfaces;
using TicketMaster.Objects;

namespace TicketMaster.Data.Services.Implementations
{
    public class VendorService : IVendorService
    {
        private TicketmasterContext _context;
        public VendorService(TicketmasterContext c)
        {
            _context = c;
        }
        public async Task CreateVendor(VendorDTO vendor)
        {
            Vendor vendorToAdd = new Vendor()
            {
                Username = vendor.Username,
                PasswordHash = vendor.PasswordHash,
                Email = vendor.Email
            };
            int locationId = await _context.Locations.Where(o => o.Id == vendor.LocationId).Select(o => o.Id).FirstAsync();
            if (locationId != null)
            {
                vendorToAdd.Locations.Add(await _context.Locations.Where(o => o.Id == vendor.LocationId).FirstAsync());
                _context.Vendors.Add(vendorToAdd);
                await _context.SaveChangesAsync();
                Console.WriteLine($"Vendor added succesfully");
            } 
            else
            {
                Console.WriteLine($"Vendor with id {vendor.LocationId} not found");
            }
        }

        public async Task DeleteVendor(int id)
        {
            var toRemove = await _context.Vendors.Where(o => o.Id == id).FirstAsync();
            if (toRemove != null)
            {
                _context.Vendors.Remove(toRemove);
                await _context.SaveChangesAsync();
                Console.WriteLine($"Vendor ({id}) deleted succesfully");
            }
            else
            {
                Console.WriteLine($"Vendor ({id}) not found");
            }
        }

        public async Task UpdateVendor(VendorDTO vendor)
        {
            var toUpdate = await _context.Vendors.Where(o => o.Id == vendor.Id).FirstAsync();
            Location location = await _context.Locations.Where(o => o.Id == vendor.LocationId).FirstAsync();
            if (toUpdate != null)
            {
                toUpdate.Email = vendor.Email;
                toUpdate.Username = vendor.Username;
                var all = await _context.Locations.ToListAsync();
                toUpdate.Locations.RemoveRange(all);
                await _context.SaveChangesAsync();
                await toUpdate.Locations.AddAsync(location);
                await _context.SaveChangesAsync();
                Console.WriteLine($"Vendor ({vendor.Id}) updated succesfully");
            }
            else
            {
                Console.WriteLine($"Vendor ({vendor.Id}) not found");
            }
        }
    }
}
