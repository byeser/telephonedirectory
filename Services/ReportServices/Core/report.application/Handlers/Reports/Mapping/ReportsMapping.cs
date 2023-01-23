using AutoMapper; 
using report.application.Handlers.Reports.Dtos; 

namespace report.application.Handlers.Reports.Mapping
{
    public class ReportsMapping : Profile
    {
        public ReportsMapping()
        { 
            CreateMap<report.domain.Entities.Persons, PersonsResponse>()
              .ReverseMap();
            CreateMap<report.domain.Entities.ContactInfo, ContactInfoResponse>()
            .ReverseMap();
        }
    }
}
