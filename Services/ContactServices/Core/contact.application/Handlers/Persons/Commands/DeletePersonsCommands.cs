using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using contact.application.Handlers.Persons.Dtos;
using contact.application.Repositories;

namespace contact.application.Handlers.Persons.Commands
{
    public class DeletePersonsCommands : IRequest<ApiResponse<bool>>
    {
        public string UUID { get; set; } 
        public class DeletePersonsCommandsHandler : IRequestHandler<DeletePersonsCommands, ApiResponse<bool>>
        {
            private readonly IPersonsRepository _personsRepository;
            private readonly IMapper _mapper;

            public DeletePersonsCommandsHandler(IPersonsRepository personsRepository, IMapper mapper)
            {
                _personsRepository = personsRepository;
                _mapper = mapper;
            }

            public async Task<ApiResponse<bool>> Handle(DeletePersonsCommands request, CancellationToken cancellationToken)
            {
                ApiResponse<bool> result = new();
                var person = await _personsRepository.GetByIdAsync(Guid.Parse(request.UUID));
                if (person is null)
                {
                    result.code = 500;
                    result.errors.Add("İşlem başarısız.");
                    result.success = new List<string>();

                    return result;
                }
                 
                await _personsRepository.RemoveAsync(person);
                result.data=true;
                result.code = 500;
                result.errors.Add("İşlem başarısız.");
                result.success = new List<string>();

                return result;
            }
        }
    }
}
