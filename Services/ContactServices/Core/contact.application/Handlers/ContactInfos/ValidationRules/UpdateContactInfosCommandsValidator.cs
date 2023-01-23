using FluentValidation;
using contact.application.Handlers.ContactInfos.Commands;


namespace contact.application.Handlers.ContactInfos.ValidationRules
{
    public class UpdateContactInfosCommandsValidator : AbstractValidator<UpdateContactInfosCommands>
    {
        public UpdateContactInfosCommandsValidator()
        {
            RuleFor(p => p.UUID)
            .NotNull()
            .NotEmpty()
            .WithMessage("Lütfen 'UUID'i boş geçmeyiniz.");
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
