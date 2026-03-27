using Microsoft.EntityFrameworkCore;
using ShipmentApp.Data;
using ShipmentApp.Models;

namespace ShipmentApp.Repositories;

public class ShipmentRepository : IShipmentRepository
{
    private readonly ApplicationDbContext _context;

    public ShipmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Shipment>> GetAllAsync()
    {
        return await _context.Shipments.ToListAsync();
    }

    public async Task<Shipment?> GetByIdAsync(int id)
    {
        return await _context.Shipments.FindAsync(id);
    }

    public async Task<Shipment?> GetByShipmentIdAsync(string shipmentId)
    {
        return await _context.Shipments.FirstOrDefaultAsync(s => s.ShipmentId == shipmentId);
    }

    public async Task AddAsync(Shipment shipment)
    {
        await _context.Shipments.AddAsync(shipment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Shipment shipment)
    {
        _context.Shipments.Update(shipment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var shipment = await GetByIdAsync(id);
        if (shipment != null)
        {
            _context.Shipments.Remove(shipment);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Shipment>> SearchAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return await GetAllAsync();

        query = query.ToLower();
        return await _context.Shipments
            .Where(s => s.ShipmentId.ToLower().Contains(query) || 
                        s.CustomerName.ToLower().Contains(query) ||
                        s.Origin.ToLower().Contains(query) ||
                        s.Destination.ToLower().Contains(query))
            .ToListAsync();
    }
}
