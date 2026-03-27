using ShipmentApp.Models;

namespace ShipmentApp.Repositories;

public interface IShipmentRepository
{
    Task<IEnumerable<Shipment>> GetAllAsync();
    Task<Shipment?> GetByIdAsync(int id);
    Task<Shipment?> GetByShipmentIdAsync(string shipmentId);
    Task AddAsync(Shipment shipment);
    Task UpdateAsync(Shipment shipment);
    Task DeleteAsync(int id);
    Task<IEnumerable<Shipment>> SearchAsync(string query);
}
