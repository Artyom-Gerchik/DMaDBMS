using System.ComponentModel.DataAnnotations;

namespace LAB6.Models;

public class RegisterModel : IValidatableObject
{
    [Required] public string Login { get; set; }

    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "FirstName")]
    public string FirstName { get; set; }

    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "LastName")]
    public string LastName { get; set; }

    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "Patronymic")]
    public string Patronymic { get; set; }

    [Required]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    [StringLength(30, MinimumLength = 6, ErrorMessage = "Length of Password should be between 6 and 30 letters")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    [Compare("Password", ErrorMessage = "Passwords doesn't match")]
    public string ConfirmPassword { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Date Of Birth")]
    public string DateOfBirth { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();

        if (FirstName.Any(x => char.IsDigit(x))) errors.Add(new ValidationResult("Name can't contain digits"));

        if (LastName.Any(x => char.IsDigit(x))) errors.Add(new ValidationResult("Surname can't contain digits"));

        if (Patronymic.Any(x => char.IsDigit(x))) errors.Add(new ValidationResult("Patronymic can't contain digits"));

        return errors;
    }
}