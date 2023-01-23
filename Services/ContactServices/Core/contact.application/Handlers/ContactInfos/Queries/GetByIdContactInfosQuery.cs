using AutoMapper;
using MediatR;
using contact.application.Handlers.ContactInfos.Dtos;
using contact.application.Repositories;

namespace contact.application.Handlers.ContactInfos.Queries
{
    public class GetByIdContactInfosQuery : IRequest<ContactInfoResponse>
    {
        public string Id { get; set; } 

        public class GetByIdContactInfosQueryHandler : IRequestHandler<GetByIdContactInfosQuery, ContactInfoResponse>
        {
            private readonly IContactInfoRepository  _contactInfoRepository; 
            private readonly IMapper _mapper;

            public GetByIdContactInfosQueryHandler(IContactInfoRepository contactInfoRepository, IMapper mapper)
            {
                _contactInfoRepository = contactInfoRepository;
                _mapper = mapper;
            }

            public async Task<ContactInfoResponse> Handle(GetByIdContactInfosQuery request, CancellationToken cancellationToken)
            {
                ContactInfoResponse result = new();
                var parties = await _contactInfoRepository.GetByIdAsync(Guid.Parse( request.Id));
                if (parties is null)
                    return result;
                result = _mapper.Map<ContactInfoResponse>(parties);
                return result;
            }
        }
    }
}
