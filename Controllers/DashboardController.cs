using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ShipmentApp.Models;
using ShipmentApp.Services;

namespace ShipmentApp.Controllers;

public class DashboardController : Controller
{
    private readonly IShipmentService _shipmentService;

    public DashboardController(IShipmentService shipmentService)
    {
        _shipmentService = shipmentService;
    }

    public async Task<IActionResult> Index()
    {
        var shipments = await _shipmentService.GetAllShipmentsAsync();
        
        var viewModel = new DashboardViewModel
        {
            TotalShipments = shipments.Count(),
            DeliveredShipments = shipments.Count(s => s.Status == "Delivered"),
            OngoingShipments = shipments.Count(s => s.Status == "In Transit" || s.Status == "Delayed"),
            UpcomingShipments = shipments.Count(s => s.Status == "Pending")
        };

        // Monthly Trend
        var currentYear = DateTime.Now.Year;
        var monthlyData = shipments
            .Where(s => s.ExpectedDeliveryDate.Year == currentYear)
            .GroupBy(s => s.ExpectedDeliveryDate.Month)
            .Select(g => new { Month = g.Key, Count = g.Count() })
            .ToDictionary(k => k.Month, v => v.Count);

        for (int i = 1; i <= 12; i++)
        {
            viewModel.MonthlyLabels.Add(new DateTime(currentYear, i, 1).ToString("MMM"));
            viewModel.MonthlyData.Add(monthlyData.ContainsKey(i) ? monthlyData[i] : 0);
        }

        // Status Distribution
        var statusGroups = shipments.GroupBy(s => s.Status);
        foreach (var group in statusGroups)
        {
            viewModel.StatusLabels.Add(group.Key);
            viewModel.StatusData.Add(group.Count());
        }

        return View(viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
