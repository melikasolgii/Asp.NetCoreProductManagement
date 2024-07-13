using eShop.Catalog.Application.Products.Contracts.DTOs;
using FluentValidation;

namespace eShop.Catalog.Application.Products.Validators
{
    public class ProductForCreateValidator : AbstractValidator<ProductForCreateDto>
    {
        public ProductForCreateValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(10);

            RuleFor(p => p.Price)
                .GreaterThan(0)
                .LessThanOrEqualTo(1000);

            RuleFor(p => p.Description)
                .MaximumLength(20);
        }
    }
}
