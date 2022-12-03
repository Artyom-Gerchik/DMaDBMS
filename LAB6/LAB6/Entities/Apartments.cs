namespace LAB6.Entities;

public class Apartments
{
    public Guid id { get; set; }
    public string? AppTypeName { get; set; }
    public int CountRooms { get; set; }
    public int CountFloors { get; set; }
    public int CountSleepingPlaces { get; set; }
    public string? AppClassName { get; set; }

}