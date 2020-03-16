using Core.Application.ViewModels.Interfaces;

using FluentValidation;

namespace Core.Application.ViewModelValidators.Implementations
{
    public class CoreViewModelValidator<TViewModel> : AbstractValidator<TViewModel>
        where TViewModel : ICoreViewModel
    {
    }
}
