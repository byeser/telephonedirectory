using FluentValidation;
using telephonedirectory.application.Handlers.Persons.Commands;


namespace telephonedirectory.application.Handlers.Persons.ValidationRules
{
    public class DeletePersonsCommandsValidator : AbstractValidator<DeletePersonsCommands>
    {
        public DeletePersonsCommandsValidator()
        {
            RuleFor(p => p.UUID)
            .NotNull()
            .NotEmpty()
            .WithMessage("Lütfen 'UUID'i boş geçmeyiniz."); 
        }
    }
}
