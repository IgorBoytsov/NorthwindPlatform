using FluentValidation;

namespace Shared.Validation.Rules.NameValidation
{
    public sealed class NameValidator<T> : AbstractValidator<T> where T : IHasName
    {
        private const string NameRegexPattern = @"^[\p{L} -]+$";

        public NameValidator(int maxLength)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя не может быть пустым.")
                .MaximumLength(maxLength).WithMessage($"Максимально допустимая длина имени {maxLength} символов.")
                .Matches(NameRegexPattern).WithMessage("Имя может содержать только буквы, пробелы и дефисы.");
        }
    }
}