using AutoMapper;
using MediatR;
using contact.application.Handlers.ContactInfos.Dtos;
using contact.application.Repositories;

namespace contact.application.Handlers.ContactInfos.Commands
{
    public class CreateContactInfosCommands : IRequest<ApiResponse<ContactInfoResponse>>
    {
        public string PersonID { get; set; }
        public int InfoType { get; set; }
        public string InfoContent { get; set; }
        public class CreateContactInfoCommandsHandler : IRequestHandler<CreateContactInfosCommands, ApiResponse<ContactInfoResponse>>
        {
            private readonly IContactInfoRepository _contactInfoRepository;
            private readonly IMapper _mapper;

            public CreateContactInfoCommandsHandler(IContactInfoRepository ContactInfoRepository, IMapper mapper)
            {
                _contactInfoRepository = ContactInfoRepository;
                _mapper = mapper;
            }

            public async Task<ApiResponse<ContactInfoResponse>> Handle(CreateContactInfosCommands request, CancellationToken cancellationToken)
            {
                ApiResponse<ContactInfoResponse> result = new();

                var chechInfoType = CheckInfoType().ContainsKey(request.InfoType);
                if (!chechInfoType)
                {
                    result.code = 500;
                    result.errors.Add("Invalid Info Type");
                    result.success = new List<string>();

                    return result;
                }
                var addContactInfo = _mapper.Map<contact.domain.Entities.ContactInfo>(request);
                if (addContactInfo is null)
                    return result;
                addContactInfo.Created = DateTime.Now;
                var ContactInfo = await _contactInfoRepository.AddAsync(addContactInfo);
                if (ContactInfo is not null)
                {
                    var addedContactInfoDto = _mapper.Map<ContactInfoResponse>(ContactInfo);
                    result.data = addedContactInfoDto;
                    return result;
                }
                result.code = 500;
                result.errors.Add("İşlem başarısız.");
                result.success = new List<string>();

                return result;
            }
            private static Dictionary<int, string> CheckInfoType()
            {
                Dictionary<int, string> dict = new() {
                    {1,"Telefon Numarası" },
                    {2,"E-mail Adresi" },
                    {3,"Konum" }
                };

                return dict;
            }
        }
    }
}
