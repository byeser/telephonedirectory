using AutoMapper;
using MediatR;
using contact.application.Handlers.Reports.Dtos;
using contact.application.Repositories;
using MassTransit;
using Microsoft.Extensions.Configuration;
using EventBus;

namespace contact.application.Handlers.Reports.Queries
{
    public class GetByIdReportsQuery : IRequest<List<PersonsResponse>>
    {
        public string Id { get; set; } 

        public class GetByIdPersonsQueryHandler : IRequestHandler<GetByIdReportsQuery, List<PersonsResponse>>
        {
            private readonly IBus _bus;
            private readonly IConfiguration _configuration;

            public GetByIdPersonsQueryHandler(IBus bus, IConfiguration configuration)
            {
                _bus = bus;
                _configuration = configuration;
            }

            public async Task<List<PersonsResponse>> Handle(GetByIdReportsQuery request, CancellationToken cancellationToken)
            {
                string mergeUri = _configuration["RabbitMQ:baseuri"]+ _configuration["RabbitMQ:personidqueue"];
                Uri uri = new Uri(mergeUri);
                var endPoint = await _bus.GetSendEndpoint(uri);
                PersonQueue personQueue = new() { PersonId= request.Id };
                await endPoint.Send(personQueue);
                return new List<PersonsResponse> { };

            }
        }
    }
}
