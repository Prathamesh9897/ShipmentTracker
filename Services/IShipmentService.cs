using ShipmentApp.Models;

namespace ShipmentApp.Services;

public interface IShipmentService
{
    Task<IEnumerable<Shipment>> GetAllShipmentsAsync();
    Task<Shipment?> GetShipmentByIdAsync(int id);
    Task CreateShipmentAsync(Shipment shipment);
    Task AutoDelayShipmentAsync(int id, int delayDays);
    Task<IEnumerable<Shipment>> SearchShipmentsAsync(string query);
}
