using AutoMapper;
using contact.application.Handlers.Persons.Commands;
using contact.application.Handlers.Persons.Mapping;
using contact.application.Handlers.Persons.Queries;
using contact.application.Repositories;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static contact.application.Handlers.Persons.Commands.CreatePersonsCommands;
using static contact.application.Handlers.Persons.Commands.DeletePersonsCommands;
using static contact.application.Handlers.Persons.Commands.UpdatePersonsCommands;
using static contact.application.Handlers.Persons.Queries.GetAllPersonsQuery;

namespace ContactServiceTest
{
    public class PersonsTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IPersonsRepository> _personRepository;
        private readonly GetAllPersonsQueryHandler _getAllPersonsQueryHandler;
        private readonly CreatePersonsCommandsHandler _createPersonsCommandsHandler;
        private readonly UpdatePersonsCommandsHandler _updatePersonsCommandsHandler;
        private readonly DeletePersonsCommandsHandler  _deletePersonsCommandsHandler;
        public PersonsTest()
        {
            _personRepository = new Mock<IPersonsRepository>();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<PersonsMapping>();
            });

            _mapper = mapperConfig.CreateMapper();
            _getAllPersonsQueryHandler = new GetAllPersonsQueryHandler(_personRepository.Object, _mapper);
            _createPersonsCommandsHandler = new CreatePersonsCommandsHandler(_personRepository.Object, _mapper);
            _updatePersonsCommandsHandler = new UpdatePersonsCommandsHandler(_personRepository.Object, _mapper);
            _deletePersonsCommandsHandler = new  DeletePersonsCommandsHandler(_personRepository.Object, _mapper);
        }
        [Test]
        public async Task GetAllAsync_Person()
        {
            var response = await _getAllPersonsQueryHandler.Handle(new GetAllPersonsQuery(), CancellationToken.None);
            Assert.True(response.Count() == 1 ? true : false);
        }
        [Test]
        public async Task AddAsync_Person()
        {
            var response = await _createPersonsCommandsHandler.Handle(new CreatePersonsCommands() { Ad = "Muhammet", Soyad = "ESER", Firma = "Rise" }, CancellationToken.None);
            Assert.True(response == null ? true : false);
        }
        [Test]
        public async Task UpdateAsync_Person()
        {
            var response = await _updatePersonsCommandsHandler.Handle(new  UpdatePersonsCommands() { UUID= "b097fc46-9282-4109-8f0c-9286cc185538", Ad = "ESER", Soyad = "Muhammet", Firma = "Rise" }, CancellationToken.None);
            Assert.True(response == null ? true : false);
        }
        [Test]
        public async Task DeleteAsync_Person()
        {
            var response = await _deletePersonsCommandsHandler.Handle(new DeletePersonsCommands() { UUID = "b097fc46-9282-4109-8f0c-9286cc185538"}, CancellationToken.None);
            Assert.True(response == null ? true : false);
        }
    }
}
