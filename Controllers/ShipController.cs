using Microsoft.AspNetCore.Mvc;
using ShipmentApp.Models;
using ShipmentApp.Services;

namespace ShipmentApp.Controllers;

public class ShipController : Controller
{
    private readonly IShipService _shipService;

    public ShipController(IShipService shipService)
    {
        _shipService = shipService;
    }

    public async Task<IActionResult> Index()
    {
        var ships = await _shipService.GetAllShipsAsync();
        return View(ships);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Ship ship)
    {
        if (ModelState.IsValid)
        {
            await _shipService.CreateShipAsync(ship);
            TempData["SuccessMessage"] = "Ship created successfully!";
            return RedirectToAction(nameof(Index));
        }
        return View(ship);
    }
}
