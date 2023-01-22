using AutoMapper;
using telephonedirectory.application.Handlers.Persons.Commands;
using telephonedirectory.application.Handlers.Persons.Dtos; 

namespace telephonedirectory.application.Handlers.Persons.Mapping
{
    public class PersonsMapping : Profile
    {
        public PersonsMapping()
        {
            CreateMap<CreatePersonsCommands, telephonedirectory.domain.Entities.Persons>()
             .ReverseMap();
            CreateMap<telephonedirectory.domain.Entities.Persons, PersonsResponse>()
              .ReverseMap();
        }
    }
}
