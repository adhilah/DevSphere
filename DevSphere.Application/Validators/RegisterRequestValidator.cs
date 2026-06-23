using DevSphere.Application.DTOs.Auth;
using FluentValidation;

namespace  DevSphere.Application.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .MinimumLength(3);
           // .maximumLength(25);
           RuleFor(x => x.Email)
               .NotEmpty()
               .Must(email => email == email.Trim())
               .WithMessage("Email cannot contain leading or trailing spaces.")
               .EmailAddress()
               .WithMessage("Please enter a valid email address.")
               .Matches(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[a-z]{2,}$")
               .WithMessage("Invalid email format.");
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .Matches("[A-Z]")
            .WithMessage("Password must contain an uppercase letter")
            .Matches("[a-z]")
            .WithMessage("Password must contain a lowercase letter")
            .Matches("[0-9]")
            .WithMessage("Password must contain a digit")
            .Matches("[^a-zA-Z0-9]")
            .WithMessage("Password must contain a special character");
    }
}