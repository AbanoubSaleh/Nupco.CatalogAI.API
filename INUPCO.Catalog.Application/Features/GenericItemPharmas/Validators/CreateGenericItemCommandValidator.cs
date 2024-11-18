using FluentValidation;
using INUPCO.Catalog.Application.Features.GenericItemPharmas.Commands.CreateGenericItem;

namespace INUPCO.Catalog.Application.Features.GenericItemPharmas.Validators;

public class CreateGenericItemCommandValidator : AbstractValidator<CreateGenericItemCommand>
{
    public CreateGenericItemCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code is required")
            .MaximumLength(50).WithMessage("Code must not exceed 50 characters");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters");

        RuleFor(x => x.Description)
            .MaximumLength(2000)
            .When(x => !string.IsNullOrEmpty(x.Description))
            .WithMessage("Description must not exceed 2000 characters");

        RuleFor(x => x.CustomerCode)
            .MaximumLength(50)
            .When(x => !string.IsNullOrEmpty(x.CustomerCode))
            .WithMessage("Customer Code must not exceed 50 characters");
    }
} 