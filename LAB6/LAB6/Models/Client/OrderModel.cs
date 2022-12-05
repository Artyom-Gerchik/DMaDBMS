using System.ComponentModel.DataAnnotations;
using LAB6.Entities;

namespace LAB6.Models;

public class OrderModel
{
    public OfferView? Offer { get; set; }
    public double? FinalPrice { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public string? ArrivingDate { get; set; }
    
    [DataType(DataType.Date)]
    public string? DepartureDate { get; set; }
    public Guid? IdToDelete { get; set; }
}