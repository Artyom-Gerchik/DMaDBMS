using System.ComponentModel.DataAnnotations;
using LAB6.Entities;

namespace LAB6.Models.Admin;

public class AddOfferModel //: IValidatableObject
{
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Please enter valid doubleNumber")]
    public double CostForDay { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Please enter valid intNumber")]
    public int Days { get; set; }

    [Required] [DataType(DataType.Text)] public string? Country { get; set; }

    [Required] [DataType(DataType.Text)] public string? Address { get; set; }

    public List<Apartments?>? Apartments { get; set; }

    public Guid? idOfSelectedApp { get; set; }

    public Guid? idForEdit { get; set; }
    
    // public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    // {
    //     var errors = new List<ValidationResult>();
    //
    //     if (CostForDay(x => char.IsDigit(x))) errors.Add(new ValidationResult("Name can't contain digits"));
    //
    //     if (LastName.Any(x => char.IsDigit(x))) errors.Add(new ValidationResult("Surname can't contain digits"));
    //
    //     if (Patronymic.Any(x => char.IsDigit(x))) errors.Add(new ValidationResult("Patronymic can't contain digits"));
    //
    //     return errors;
    // }
    
}