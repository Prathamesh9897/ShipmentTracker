namespace ShipmentApp.Models;

public class Shipment
{
    public int Id { get; set; }
    public string ShipmentId { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending";
    public DateTime ExpectedDeliveryDate { get; set; }
}
