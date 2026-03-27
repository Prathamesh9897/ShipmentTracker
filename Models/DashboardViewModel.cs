using System.Collections.Generic;

namespace ShipmentApp.Models;

public class DashboardViewModel
{
    public int TotalShipments { get; set; }
    public int UpcomingShipments { get; set; }
    public int OngoingShipments { get; set; }
    public int DeliveredShipments { get; set; }

    public List<string> MonthlyLabels { get; set; } = new();
    public List<int> MonthlyData { get; set; } = new();

    public List<string> StatusLabels { get; set; } = new();
    public List<int> StatusData { get; set; } = new();
}
