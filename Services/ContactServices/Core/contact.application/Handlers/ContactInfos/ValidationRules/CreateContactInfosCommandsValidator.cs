using FluentValidation;
using contact.application.Handlers.ContactInfos.Commands;

namespace contact.application.Handlers.ContactInfos.ValidationRules
{
    public class CreateContactInfosCommandsValidator : AbstractValidator<CreateContactInfosCommands>
    {
        public CreateContactInfosCommandsValidator()
        {
            RuleFor(p => p.PersonID)
             .NotNull()
             .NotEmpty()
             .WithMessage("Lütfen 'PersonID'i boş geçmeyiniz.");
            RuleFor(p => p.InfoType)
             .NotNull()
             .NotEmpty()
             .WithMessage("Lütfen 'InfoType'i boş geçmeyiniz.");
            RuleFor(p => p.InfoContent)
             .NotNull()
             .NotEmpty()
             .WithMessage("Lütfen 'InfoContent'i boş geçmeyiniz.");
        }
    }
}
