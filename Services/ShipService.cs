using ShipmentApp.Models;
using ShipmentApp.Repositories;

namespace ShipmentApp.Services;

public class ShipService : IShipService
{
    private readonly IShipRepository _repository;

    public ShipService(IShipRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Ship>> GetAllShipsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Ship?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task CreateShipAsync(Ship ship)
    {
        await _repository.AddAsync(ship);
    }

    public async Task UpdateShipAsync(Ship ship)
    {
        await _repository.UpdateAsync(ship);
    }

    public async Task DeleteShipAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<Ship?> GetShipByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }
}
