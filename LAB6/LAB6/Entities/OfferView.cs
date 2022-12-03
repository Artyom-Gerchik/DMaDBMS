namespace LAB6.Entities;

public class OfferView
{
    public Guid id { get; set; }
    public double CostForDay { get; set; }
    public int Days { get; set; }
    public string? Country { get; set; }
    public string? Address { get; set; }
    public string? AppTypeName { get; set; }
    public int CountRooms { get; set; }
    public int CountFloors { get; set; }
    public int CountSleepingPlaces { get; set; }
    public string? AppClassName { get; set; }
}