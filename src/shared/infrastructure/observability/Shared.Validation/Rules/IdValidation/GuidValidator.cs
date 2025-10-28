using FluentValidation;

namespace Shared.Validation.Rules.IdValidation
{
    public sealed class GuidValidator<T> : AbstractValidator<T> where T : IHasGuidId
    {
        public GuidValidator()
            => RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Идентификатор (GUID) не может быть пустым");
    }
}