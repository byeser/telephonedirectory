using AutoMapper;
using MediatR;
using contact.application.Handlers.ContactInfos.Dtos;
using contact.application.Repositories;

namespace contact.application.Handlers.ContactInfos.Queries
{
    public class GetAllContactInfosQuery : IRequest<List<ContactInfoResponse>>
    {
        public string PersonId { get; set; }
        public class GetAllContactInfosQueryHandler : IRequestHandler<GetAllContactInfosQuery, List<ContactInfoResponse>>
        {
            private readonly IContactInfoRepository  _contactInfosRepository;          
            private readonly IMapper _mapper;

            public GetAllContactInfosQueryHandler(IContactInfoRepository contactInfosRepository, IMapper mapper)
            {
                _contactInfosRepository = contactInfosRepository;
                _mapper = mapper;
            }

            public async Task<List<ContactInfoResponse>> Handle(GetAllContactInfosQuery request, CancellationToken cancellationToken)
            {
                List<ContactInfoResponse> result = new();
                var assign = await _contactInfosRepository.GetWhereAsync(x=>x.PersonID==request.PersonId);
                if (assign is not {Count:>0 })
                    return result;

                result = _mapper.Map<List<ContactInfoResponse>>(assign); 
                return result;
            }
        }
    }
}
