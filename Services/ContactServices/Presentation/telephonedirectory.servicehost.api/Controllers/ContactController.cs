using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using telephonedirectory.application.Handlers.Persons.Commands;
using telephonedirectory.application.Handlers.Persons.Queries;

namespace telephonedirectory.servicehost.api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [EnableCors("AllowOrigin")]
    [Produces("application/json")]

    public class ContactController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [Route("{Id}")]
        [HttpGet]
        public async Task<IActionResult> Get(string Id)
        {
            var response = await Mediator.Send(new GetByIdPersonsQuery() { Id = Id });
            return Ok(response);
        }
        [Route("Get")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await Mediator.Send(new GetAllPersonsQuery());
            return Ok(response);
        }
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreatePersonsCommands request)
        {
            var response = await Mediator.Send(request);
            return Ok(response);
        }
        [Route("Update")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatePersonsCommands request)
        {
            var response = await Mediator.Send(request);
            return Ok(response);
        }
        [Route("{Id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string Id)
        {
            var response = await Mediator.Send(new DeletePersonsCommands() { UUID = Id });
            return Ok(response);
        }
    }
}
