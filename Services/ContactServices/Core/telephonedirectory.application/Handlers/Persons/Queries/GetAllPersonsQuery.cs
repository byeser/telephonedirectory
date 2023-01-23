using AutoMapper;
using MediatR;
using telephonedirectory.application.Handlers.Persons.Dtos;
using telephonedirectory.application.Repositories;

namespace telephonedirectory.application.Handlers.Persons.Queries
{
    public class GetAllPersonsQuery : IRequest<List<PersonsResponse>>
    { 
        public class GetAllPersonsQueryHandler : IRequestHandler<GetAllPersonsQuery, List<PersonsResponse>>
        {
            private readonly IPersonsRepository  _personsRepository;          
            private readonly IMapper _mapper;

            public GetAllPersonsQueryHandler(IPersonsRepository personsRepository, IMapper mapper)
            {
                _personsRepository = personsRepository;
                _mapper = mapper;
            }

            public async Task<List<PersonsResponse>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
            {
                List<PersonsResponse> result = new();
                var assign = await _personsRepository.GetAsync();
                if (assign is not {Count:>0 })
                    return result;

                result = _mapper.Map<List<PersonsResponse>>(assign); 
                return result;
            }
        }
    }
}
