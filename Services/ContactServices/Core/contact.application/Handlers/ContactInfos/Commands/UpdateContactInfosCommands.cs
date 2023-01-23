using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using contact.application.Handlers.ContactInfos.Dtos;
using contact.application.Repositories;

namespace contact.application.Handlers.ContactInfos.Commands
{
    public class UpdateContactInfosCommands : IRequest<ApiResponse<ContactInfoResponse>>
    {
        public string UUID { get; set; } 
        public int InfoType { get; set; }
        public string InfoContent { get; set; }
        public class UpdateContactInfosCommandsHandler : IRequestHandler<UpdateContactInfosCommands, ApiResponse<ContactInfoResponse>>
        {
            private readonly IContactInfoRepository _contactInfosRepository;
            private readonly IMapper _mapper;

            public UpdateContactInfosCommandsHandler(IContactInfoRepository contactInfosRepository, IMapper mapper)
            {
                _contactInfosRepository = contactInfosRepository;
                _mapper = mapper;
            }

            public async Task<ApiResponse<ContactInfoResponse>> Handle(UpdateContactInfosCommands request, CancellationToken cancellationToken)
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
                var person = await _contactInfosRepository.GetByIdAsync(Guid.Parse(request.UUID));
                if (person is null)
                {
                    result.code = 500;
                    result.errors.Add("İşlem başarısız.");
                    result.success = new List<string>();

                    return result;
                }
                person.InfoType = request.InfoType;
                person.InfoContent= request.InfoContent;
               
                var ContactInfosUpdate = await _contactInfosRepository.UpdateAsync(person);
                if (ContactInfosUpdate is not null)
                {
                    var updatedContactInfosDto = _mapper.Map<ContactInfoResponse>(ContactInfosUpdate);
                    result.data = updatedContactInfosDto;
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
