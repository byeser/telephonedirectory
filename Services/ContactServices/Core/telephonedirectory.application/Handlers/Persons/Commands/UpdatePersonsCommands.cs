using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using telephonedirectory.application.Handlers.Persons.Dtos;
using telephonedirectory.application.Repositories;

namespace telephonedirectory.application.Handlers.Persons.Commands
{
    public class UpdatePersonsCommands : IRequest<ApiResponse<PersonsResponse>>
    {
        public string UUID { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Firma { get; set; }
        public class UpdatePersonsCommandsHandler : IRequestHandler<UpdatePersonsCommands, ApiResponse<PersonsResponse>>
        {
            private readonly IPersonsRepository _personsRepository;
            private readonly IMapper _mapper;

            public UpdatePersonsCommandsHandler(IPersonsRepository personsRepository, IMapper mapper)
            {
                _personsRepository = personsRepository;
                _mapper = mapper;
            }

            public async Task<ApiResponse<PersonsResponse>> Handle(UpdatePersonsCommands request, CancellationToken cancellationToken)
            {
                ApiResponse<PersonsResponse> result = new();
                var person = await _personsRepository.GetByIdAsync(Guid.Parse(request.UUID));
                if (person is null)
                {
                    result.code = 500;
                    result.errors.Add("İşlem başarısız.");
                    result.success = new List<string>();

                    return result;
                }

                person.Ad = request.Ad;
                person.Soyad= request.Soyad;
                person.Firma= request.Firma;
                var personsUpdate = await _personsRepository.UpdateAsync(person);
                if (personsUpdate is not null)
                {
                    var updatedPersonsDto = _mapper.Map<PersonsResponse>(personsUpdate);
                    result.data = updatedPersonsDto;
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
