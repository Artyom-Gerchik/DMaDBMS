namespace LAB6.Entities;

public class Log
{
    public Guid id { get; set; }
    public Guid clientId { get; set; }
    public DateTime happenedAt { get; set; }
    public string? logType { get; set; }
}