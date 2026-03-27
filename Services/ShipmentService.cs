using ShipmentApp.Models;
using ShipmentApp.Repositories;

namespace ShipmentApp.Services;

public class ShipmentService : IShipmentService
{
    private readonly IShipmentRepository _repository;

    public ShipmentService(IShipmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Shipment>> GetAllShipmentsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Shipment?> GetShipmentByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task CreateShipmentAsync(Shipment shipment)
    {
        if (string.IsNullOrWhiteSpace(shipment.ShipmentId))
        {
            shipment.ShipmentId = $"TRK-{Random.Shared.Next(10000, 99999)}";
        }
        await _repository.AddAsync(shipment);
    }

    public async Task AutoDelayShipmentAsync(int id, int delayDays)
    {
        var shipment = await _repository.GetByIdAsync(id);
        if (shipment != null)
        {
            shipment.Status = "Delayed";
            shipment.ExpectedDeliveryDate = shipment.ExpectedDeliveryDate.AddDays(delayDays);
            await _repository.UpdateAsync(shipment);
        }
    }

    public async Task<IEnumerable<Shipment>> SearchShipmentsAsync(string query)
    {
        return await _repository.SearchAsync(query);
    }
}
