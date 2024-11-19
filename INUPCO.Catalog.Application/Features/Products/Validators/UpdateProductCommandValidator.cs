using FluentValidation;
using INUPCO.Catalog.Application.Features.Products.Commands.UpdateProduct;

namespace INUPCO.Catalog.Application.Features.Products.Validators;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Valid Id is required");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters");

        RuleFor(x => x.TradeCode)
            .NotEmpty().WithMessage("TradeCode is required")
            .MaximumLength(50).WithMessage("TradeCode must not exceed 50 characters");
    }
} 