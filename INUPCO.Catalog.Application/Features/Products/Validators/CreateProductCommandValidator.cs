using FluentValidation;
using INUPCO.Catalog.Application.Features.Products.Commands.CreateProduct;

namespace INUPCO.Catalog.Application.Features.Products.Validators;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters");

        RuleFor(x => x.ManufacturerId)
            .GreaterThan(0).WithMessage("Valid ManufacturerId is required");

        RuleFor(x => x.TradeCode)
            .NotEmpty().WithMessage("TradeCode is required")
            .MaximumLength(50).WithMessage("TradeCode must not exceed 50 characters");

        RuleFor(x => x.SubsidiaryId)
            .GreaterThan(0).WithMessage("Valid SubsidiaryId is required");
    }
} 