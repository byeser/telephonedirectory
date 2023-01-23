using AutoMapper; 
using contact.application.Handlers.Reports.Dtos; 

namespace contact.application.Handlers.Reports.Mapping
{
    public class ReportsMapping : Profile
    {
        public ReportsMapping()
        { 
            CreateMap<contact.domain.Entities.Persons, PersonsResponse>()
              .ReverseMap();
            CreateMap<contact.domain.Entities.ContactInfo, ContactInfoResponse>()
            .ReverseMap();
        }
    }
}
