using AutoMapper;
using contact.application.Handlers.Persons.Commands;
using contact.application.Handlers.Persons.Dtos; 

namespace contact.application.Handlers.Persons.Mapping
{
    public class PersonsMapping : Profile
    {
        public PersonsMapping()
        {
            CreateMap<CreatePersonsCommands, contact.domain.Entities.Persons>()
             .ReverseMap();
            CreateMap<contact.domain.Entities.Persons, PersonsResponse>()
              .ReverseMap();
        }
    }
}
