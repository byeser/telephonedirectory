using AutoMapper;
using MediatR;
using contact.application.Handlers.Persons.Dtos;
using contact.application.Repositories;

namespace contact.application.Handlers.Persons.Queries
{
    public class GetByIdPersonsQuery : IRequest<PersonsResponse>
    {
        public string Id { get; set; } 

        public class GetByIdPersonsQueryHandler : IRequestHandler<GetByIdPersonsQuery, PersonsResponse>
        {
            private readonly IPersonsRepository  _personsRepository; 
            private readonly IMapper _mapper;

            public GetByIdPersonsQueryHandler(IPersonsRepository personsRepository, IMapper mapper)
            {
                _personsRepository = personsRepository;
                _mapper = mapper;
            }

            public async Task<PersonsResponse> Handle(GetByIdPersonsQuery request, CancellationToken cancellationToken)
            {
                PersonsResponse result = new();
                var parties = await _personsRepository.GetByIdAsync(Guid.Parse( request.Id));
                if (parties is null)
                    return result;
                result = _mapper.Map<PersonsResponse>(parties);
                return result;
            }
        }
    }
}
