using System.ComponentModel.DataAnnotations;

namespace ShipmentApp.Models;

public class Ship
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Ship Name")]
    public string ShipName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Ship Code")]
    public string ShipCode { get; set; } = string.Empty;

    [Display(Name = "IMO Number")]
    public string IMONumber { get; set; } = string.Empty;

    public int Capacity { get; set; }

    [Display(Name = "Ship Type")]
    public string ShipType { get; set; } = string.Empty;

    [Display(Name = "Flag Country")]
    public string FlagCountry { get; set; } = string.Empty;

    [Display(Name = "Year Built")]
    public int YearBuilt { get; set; }

    public string Status { get; set; } = "Active"; // Active/Inactive
}
