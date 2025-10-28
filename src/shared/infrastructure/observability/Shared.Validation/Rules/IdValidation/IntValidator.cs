using FluentValidation;

namespace Shared.Validation.Rules.IdValidation
{
    public sealed class IntValidator<T> : AbstractValidator<T> where T : IHasIntId
    {
        public IntValidator()
            => RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Идентификатор (Int) должен быть больше 0");
    }
}