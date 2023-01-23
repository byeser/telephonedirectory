using FluentValidation;
using contact.application.Handlers.ContactInfos.Commands;


namespace contact.application.Handlers.ContactInfos.ValidationRules
{
    public class DeleteContactInfosCommandsValidator : AbstractValidator<DeleteContactInfosCommands>
    {
        public DeleteContactInfosCommandsValidator()
        {
            RuleFor(p => p.UUID)
            .NotNull()
            .NotEmpty()
            .WithMessage("Lütfen 'UUID'i boş geçmeyiniz."); 
        }
    }
}
