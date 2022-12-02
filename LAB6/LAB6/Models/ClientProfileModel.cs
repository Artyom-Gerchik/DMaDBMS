namespace LAB6.Models;

public class ClientProfileModel
{
    public string? Login { get; set; }
    public string? Password { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Patronymic { get; set; }
    public DateOnly DateOfBirth { get; set; }
}