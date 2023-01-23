using FluentValidation;
using telephonedirectory.application.Handlers.Persons.Commands;


namespace telephonedirectory.application.Handlers.Persons.ValidationRules
{
    public class UpdatePersonsCommandsValidator : AbstractValidator<UpdatePersonsCommands>
    {
        public UpdatePersonsCommandsValidator()
        {
            RuleFor(p => p.UUID)
            .NotNull()
            .NotEmpty()
            .WithMessage("Lütfen 'UUID'i boş geçmeyiniz.");
            RuleFor(p => p.Ad)
               .NotNull()
               .NotEmpty()
               .WithMessage("Lütfen 'Ad'i boş geçmeyiniz.");
            RuleFor(p => p.Soyad)
             .NotNull()
             .NotEmpty()
             .WithMessage("Lütfen 'Soyad'i boş geçmeyiniz.");
            RuleFor(p => p.Firma)
             .NotNull()
             .NotEmpty()
             .WithMessage("Lütfen 'Firma'i boş geçmeyiniz.");
        }
    }
}
