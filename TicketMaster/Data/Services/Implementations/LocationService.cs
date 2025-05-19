using Microsoft.EntityFrameworkCore;
using Ticketmaster.Data.Services.Interfaces;
using Ticketmaster.Objects;
using Ticketmaster.Data.DTOs;
using System.Numerics;

namespace Ticketmaster.Data.Services.Implementations
{
    public class LocationService : ILocationService
    {
        private TicketmasterContext _context;
        public LocationService(TicketmasterContext c) 
        {
            _context = c;
        }
        public async Task CreateLocation(LocationDTO location)
        {
            Vendor vendor = await _context.Vendors.Where(o => o.Id == location.VendorId).FirstAsync();

            Location addLocation = new Location()
            {
                Address = location.Address,
                Capacity = (int)location.Capacity,
                Name = location.Name
            };

            addLocation.Vendors.Add(vendor);

            await _context.Locations.AddAsync(addLocation);
            await _context.SaveChangesAsync();
            Console.WriteLine($"Added new location succesfully");
        }

        public async Task DeleteLocation(int id)
        {
            var toRemove = await _context.Locations.Where(o => o.Id == id).FirstAsync();
            if (toRemove != null)
            {
                _context.Locations.Remove(toRemove);
                Console.WriteLine($"Location ({id}) succesfully removed");
            }
            else
            {
                Console.WriteLine($"Location ({id}) not found");
            }
        }

        public async Task UpdateLocation(LocationDTO location)
        {
            var toUpdate = await _context.Locations.Where(o => o.Id == (int)location.Id).FirstAsync();
            Vendor vendor = await _context.Vendors.Where(o => o.Id == location.VendorId).FirstAsync();
            if (toUpdate != null)
            {
                toUpdate.Address = location.Address;
                toUpdate.Capacity = (int)location.Capacity;
                toUpdate.Name = location.Name;
                toUpdate.Capacity = (int)location.Capacity;
                
                await _context.SaveChangesAsync();
                Console.WriteLine($"Location ({location.Id}) succesfully updated");
            }
            else
            {
                Console.WriteLine($"Location ({location.Id}) not found");
            }
        }
    }
}
