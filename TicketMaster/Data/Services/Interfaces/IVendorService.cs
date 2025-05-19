using TicketMaster.Data.DTOs;

namespace TicketMaster.Data.Services.Interfaces
{
    public interface IVendorService
    {
        public Task CreateVendor(VendorDTO vendor);
        public Task DeleteVendor(int id);
        public Task UpdateVendor(VendorDTO vendor);
    }
}
