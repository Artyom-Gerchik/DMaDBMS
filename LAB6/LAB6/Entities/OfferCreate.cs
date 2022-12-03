namespace LAB6.Entities;

public class OfferCreate
{
    public Guid id { get; set; }
    public Guid? AppId { get; set; }
    public double CostForDay { get; set; }
    public int Days { get; set; }
    public string? Country { get; set; }
    public string? Address { get; set; }
}