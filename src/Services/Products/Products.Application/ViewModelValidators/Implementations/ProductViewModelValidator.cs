using Core.Application.ViewModelValidators.Implementations;

using FluentValidation;

using Products.Application.ViewModels.Implementations;

namespace Products.Application.ViewModelValidators.Implementations
{
    public class ProductViewModelValidator : CoreViewModelValidator<ProductViewModel>
    {
        public ProductViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage($"Field {nameof(ProductViewModel.Name)} is required.");
            RuleFor(x => x.Name).MaximumLength(30).WithMessage($"Maximum character length for field {nameof(ProductViewModel.Name)} is 30.");
            RuleFor(x => x.Description).MaximumLength(100).WithMessage($"Maximum character length for field {nameof(ProductViewModel.Description)} is 100.");
        }
    }
}
