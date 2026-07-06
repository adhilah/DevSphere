using FluentValidation;
using DevSphere.Application.Features.Technologies.Commands.CreateTechnology;


namespace DevSphere.Application.Features.Technologies.Commands.CreateTechnology;

public sealed class CreateTechnologyCommandValidator
    : AbstractValidator<CreateTechnologyCommand>
{
    public CreateTechnologyCommandValidator()
    {
        RuleFor(x => x.CategoryId)
            .GreaterThan(0)
            .WithMessage("Category is required.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(1000);

        RuleFor(x => x.ImageUrl)
            .MaximumLength(500);

        RuleFor(x => x.Position)
            .GreaterThanOrEqualTo(0);
        }
}