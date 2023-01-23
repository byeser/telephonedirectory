using AutoMapper;
using contact.application.Handlers.ContactInfos.Commands;
using contact.application.Handlers.ContactInfos.Dtos; 

namespace contact.application.Handlers.ContactInfos.Mapping
{
    public class ContactInfosMapping : Profile
    {
        public ContactInfosMapping()
        {
            CreateMap<CreateContactInfosCommands, contact.domain.Entities.ContactInfo>()
             .ReverseMap();
            CreateMap<contact.domain.Entities.ContactInfo, ContactInfoResponse>()
              .ReverseMap();
        }
    }
}
