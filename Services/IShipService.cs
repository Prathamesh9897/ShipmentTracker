using ShipmentApp.Models;

namespace ShipmentApp.Services;

public interface IShipService
{
    Task<IEnumerable<Ship>> GetAllShipsAsync();
    Task<Ship?> GetShipByIdAsync(int id);
    Task CreateShipAsync(Ship ship);
    Task UpdateShipAsync(Ship ship);
    Task DeleteShipAsync(int id);
}
