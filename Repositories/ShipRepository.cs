using Microsoft.EntityFrameworkCore;
using ShipmentApp.Data;
using ShipmentApp.Models;

namespace ShipmentApp.Repositories;

public class ShipRepository : IShipRepository
{
    private readonly ApplicationDbContext _context;

    public ShipRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Ship>> GetAllAsync()
    {
        return await _context.Ships.ToListAsync();
    }

    public async Task<Ship?> GetByIdAsync(int id)
    {
        return await _context.Ships.FindAsync(id);
    }

    public async Task AddAsync(Ship ship)
    {
        await _context.Ships.AddAsync(ship);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Ship ship)
    {
        _context.Ships.Update(ship);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var ship = await _context.Ships.FindAsync(id);
        if (ship != null)
        {
            _context.Ships.Remove(ship);
            await _context.SaveChangesAsync();
        }
    }
}
