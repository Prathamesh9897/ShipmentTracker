using Microsoft.AspNetCore.Mvc;
using ShipmentApp.Models;
using ShipmentApp.Services;

namespace ShipmentApp.Controllers;

public class ShipmentController : Controller
{
    private readonly IShipmentService _shipmentService;

    public ShipmentController(IShipmentService shipmentService)
    {
        _shipmentService = shipmentService;
    }

    public async Task<IActionResult> Index(string query)
    {
        ViewData["CurrentFilter"] = query;
        var shipments = await _shipmentService.SearchShipmentsAsync(query);
        return View(shipments);
    }

    public IActionResult Create()
    {
        return View(new Shipment { ExpectedDeliveryDate = DateTime.Now.AddDays(3) });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Shipment shipment)
    {
        if (ModelState.IsValid)
        {
            await _shipmentService.CreateShipmentAsync(shipment);
            return RedirectToAction(nameof(Index));
        }
        return View(shipment);
    }

    [HttpPost]
    public async Task<IActionResult> AutoDelay(int id)
    {
        await _shipmentService.AutoDelayShipmentAsync(id, 2); // default delay of 2 days
        return RedirectToAction(nameof(Index));
    }
}
