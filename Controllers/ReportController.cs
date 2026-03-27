using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using ShipmentApp.Services;

namespace ShipmentApp.Controllers;

public class ReportController : Controller
{
    private readonly IShipmentService _shipmentService;
    private readonly ICustomerService _customerService;

    public ReportController(IShipmentService shipmentService, ICustomerService customerService)
    {
        _shipmentService = shipmentService;
        _customerService = customerService;
    }

    // Shipment Report
    public async Task<IActionResult> ShipmentReport(DateTime? startDate, DateTime? endDate, string? status)
    {
        var shipments = await _shipmentService.GetAllShipmentsAsync();

        if (startDate.HasValue)
            shipments = shipments.Where(s => s.ExpectedDeliveryDate >= startDate.Value);

        if (endDate.HasValue)
            shipments = shipments.Where(s => s.ExpectedDeliveryDate <= endDate.Value);

        if (!string.IsNullOrEmpty(status))
            shipments = shipments.Where(s => s.Status == status);

        ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
        ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");
        ViewBag.Status = status;

        return View(shipments);
    }

    public async Task<IActionResult> ExportShipmentsExcel(DateTime? startDate, DateTime? endDate, string? status)
    {
        var shipments = await _shipmentService.GetAllShipmentsAsync();

        if (startDate.HasValue)
            shipments = shipments.Where(s => s.ExpectedDeliveryDate >= startDate.Value);

        if (endDate.HasValue)
            shipments = shipments.Where(s => s.ExpectedDeliveryDate <= endDate.Value);

        if (!string.IsNullOrEmpty(status))
            shipments = shipments.Where(s => s.Status == status);

        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Shipments");
            var currentRow = 1;

            worksheet.Cell(currentRow, 1).Value = "Shipment ID";
            worksheet.Cell(currentRow, 2).Value = "Customer Name";
            worksheet.Cell(currentRow, 3).Value = "Origin";
            worksheet.Cell(currentRow, 4).Value = "Destination";
            worksheet.Cell(currentRow, 5).Value = "Status";
            worksheet.Cell(currentRow, 6).Value = "Expected Delivery";

            worksheet.Range("A1:F1").Style.Font.Bold = true;
            worksheet.Range("A1:F1").Style.Fill.BackgroundColor = XLColor.LightBlue;

            foreach (var shipment in shipments)
            {
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = shipment.ShipmentId;
                worksheet.Cell(currentRow, 2).Value = shipment.CustomerName;
                worksheet.Cell(currentRow, 3).Value = shipment.Origin;
                worksheet.Cell(currentRow, 4).Value = shipment.Destination;
                worksheet.Cell(currentRow, 5).Value = shipment.Status;
                worksheet.Cell(currentRow, 6).Value = shipment.ExpectedDeliveryDate.ToShortDateString();
            }

            worksheet.Columns().AdjustToContents();

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"ShipmentReport_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
            }
        }
    }

    // Customer Report
    public async Task<IActionResult> CustomerReport()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return View(customers);
    }

    public async Task<IActionResult> ExportCustomersExcel()
    {
        var customers = await _customerService.GetAllCustomersAsync();

        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Customers");
            var currentRow = 1;

            worksheet.Cell(currentRow, 1).Value = "Customer Name";
            worksheet.Cell(currentRow, 2).Value = "Company Name";
            worksheet.Cell(currentRow, 3).Value = "Email";
            worksheet.Cell(currentRow, 4).Value = "Phone";
            worksheet.Cell(currentRow, 5).Value = "Location";
            worksheet.Cell(currentRow, 6).Value = "Added Date";

            worksheet.Range("A1:F1").Style.Font.Bold = true;
            worksheet.Range("A1:F1").Style.Fill.BackgroundColor = XLColor.LightBlue;

            foreach (var customer in customers)
            {
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = customer.CustomerName;
                worksheet.Cell(currentRow, 2).Value = customer.CompanyName;
                worksheet.Cell(currentRow, 3).Value = customer.Email;
                worksheet.Cell(currentRow, 4).Value = customer.Phone;
                worksheet.Cell(currentRow, 5).Value = $"{customer.City}, {customer.Country}";
                worksheet.Cell(currentRow, 6).Value = customer.CreatedDate.ToShortDateString();
            }

            worksheet.Columns().AdjustToContents();

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"CustomerReport_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
            }
        }
    }
}
