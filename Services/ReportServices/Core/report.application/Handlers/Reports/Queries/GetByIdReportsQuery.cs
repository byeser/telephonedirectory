using AutoMapper;
using MediatR;
using report.application.Handlers.Reports.Dtos;
using report.application.Repositories;

namespace report.application.Handlers.Reports.Queries
{
    public class GetByIdReportsQuery : IRequest<List<PersonsResponse>>
    {
        public string Id { get; set; } 

        public class GetByIdPersonsQueryHandler : IRequestHandler<GetByIdReportsQuery, List<PersonsResponse>>
        {
            private readonly IContactInfoRepository _contactInfoRepository;
            private readonly IPersonsRepository _personsRepository;
            private readonly IMapper _mapper;

            public GetByIdPersonsQueryHandler(IContactInfoRepository contactInfoRepository, IPersonsRepository personsRepository, IMapper mapper)
            {
                _contactInfoRepository = contactInfoRepository;
                _personsRepository = personsRepository;
                _mapper = mapper;
            }

            public async Task<List<PersonsResponse>> Handle(GetByIdReportsQuery request, CancellationToken cancellationToken)
            {
                List<PersonsResponse> result = new();
                var personList = await _personsRepository.GetWhereAsync(x=>x.UUID==Guid.Parse(request.Id));
                if (personList is not { Count: > 0 })
                    return result;
                var dict = personList.ToDictionary(x => x.UUID.ToString(), y => y);
                foreach (var person in dict.Keys)
                {
                    var contactInfo = await _contactInfoRepository.GetWhereAsync(x => x.PersonID == person);
                    var per = _mapper.Map<PersonsResponse>(dict[person]);
                    per.Contact = _mapper.Map<List<ContactInfoResponse>>(contactInfo);
                    result.Add(per);
                }
                return result;
            }
        }
    }
}
