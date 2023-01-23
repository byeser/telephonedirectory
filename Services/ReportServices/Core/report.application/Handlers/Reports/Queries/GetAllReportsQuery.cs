using AutoMapper;
using MediatR;
using report.application.Handlers.Reports.Dtos;
using report.application.Repositories;

namespace report.application.Handlers.Reports.Queries
{
    public class GetAllReportsQuery : IRequest<List<PersonsResponse>>
    {
        public class GetAllPersonsQueryHandler : IRequestHandler<GetAllReportsQuery, List<PersonsResponse>>
        {
            private readonly IContactInfoRepository _contactInfoRepository;
            private readonly IPersonsRepository _personsRepository;
            private readonly IMapper _mapper;

            public GetAllPersonsQueryHandler(IPersonsRepository personsRepository, IMapper mapper, IContactInfoRepository contactInfoRepository)
            {
                _personsRepository = personsRepository;
                _mapper = mapper;
                _contactInfoRepository = contactInfoRepository;
            }

            public async Task<List<PersonsResponse>> Handle(GetAllReportsQuery request, CancellationToken cancellationToken)
            {
                List<PersonsResponse> result = new(); 
                var personList = await _personsRepository.GetAsync();
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
