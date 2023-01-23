using AutoMapper;
using MediatR;
using telephonedirectory.application.Handlers.Persons.Dtos;
using telephonedirectory.application.Repositories;

namespace telephonedirectory.application.Handlers.Persons.Commands
{
    public class CreatePersonsCommands : IRequest<ApiResponse<PersonsResponse>>
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Firma { get; set; }
        public class CreatePersonsCommandsHandler : IRequestHandler<CreatePersonsCommands, ApiResponse<PersonsResponse>>
        {
            private readonly IPersonsRepository _personsRepository;
            private readonly IMapper _mapper;

            public CreatePersonsCommandsHandler(IPersonsRepository personsRepository, IMapper mapper)
            {
                _personsRepository = personsRepository;
                _mapper = mapper;
            }

            public async Task<ApiResponse<PersonsResponse>> Handle(CreatePersonsCommands request, CancellationToken cancellationToken)
            {
                ApiResponse<PersonsResponse> result = new();               

                var addPersons = _mapper.Map<telephonedirectory.domain.Entities.Persons>(request);
                if (addPersons is null)
                    return result;

                var Persons = await _personsRepository.AddAsync(addPersons);
                if (Persons is not null)
                {
                    var addedPersonsDto = _mapper.Map<PersonsResponse>(Persons);
                    result.data = addedPersonsDto;
                    return result;
                }
                result.code = 500;
                result.errors.Add("İşlem başarısız.");
                result.success = new List<string>();

                return result;
            }
        }
    }
}
