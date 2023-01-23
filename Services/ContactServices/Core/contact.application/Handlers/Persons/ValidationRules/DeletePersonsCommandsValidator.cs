using FluentValidation;
using contact.application.Handlers.Persons.Commands;


namespace contact.application.Handlers.Persons.ValidationRules
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
