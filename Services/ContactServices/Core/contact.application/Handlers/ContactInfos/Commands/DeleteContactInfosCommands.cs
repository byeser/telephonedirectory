using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using contact.application.Handlers.ContactInfos.Dtos;
using contact.application.Repositories;

namespace contact.application.Handlers.ContactInfos.Commands
{
    public class DeleteContactInfosCommands : IRequest<ApiResponse<bool>>
    {
        public string UUID { get; set; } 
        public class DeleteContactInfosCommandsHandler : IRequestHandler<DeleteContactInfosCommands, ApiResponse<bool>>
        {
            private readonly IContactInfoRepository _contactInfosRepository;
            private readonly IMapper _mapper;

            public DeleteContactInfosCommandsHandler(IContactInfoRepository contactInfosRepository, IMapper mapper)
            {
                _contactInfosRepository = contactInfosRepository;
                _mapper = mapper;
            }

            public async Task<ApiResponse<bool>> Handle(DeleteContactInfosCommands request, CancellationToken cancellationToken)
            {
                ApiResponse<bool> result = new();
                var person = await _contactInfosRepository.GetByIdAsync(Guid.Parse(request.UUID));
                if (person is null)
                {
                    result.code = 500;
                    result.errors.Add("İşlem başarısız.");
                    result.success = new List<string>();

                    return result;
                }
                 
                await _contactInfosRepository.RemoveAsync(person);
                result.data=true;
                result.code = 500;
                result.errors.Add("İşlem başarısız.");
                result.success = new List<string>();

                return result;
            }
        }
    }
}
