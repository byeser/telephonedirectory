using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using contact.application.Handlers.ContactInfos.Commands;
using contact.application.Handlers.ContactInfos.Queries;
using contact.application.Handlers.ContactInfos.Queries;

namespace contact.servicehost.api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [EnableCors("AllowOrigin")]
    [Produces("application/json")]

    public class InfoController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [Route("{Id}")]
        [HttpGet]
        public async Task<IActionResult> Get(string Id)
        {
            var response = await Mediator.Send(new GetByIdContactInfosQuery() { Id = Id });
            return Ok(response);
        }
        [Route("Person/{PersonId}")]
        [HttpGet]
        public async Task<IActionResult> Person(string PersonId)
        {
            var response = await Mediator.Send(new GetAllContactInfosQuery() {PersonId=PersonId });
            return Ok(response);
        }
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateContactInfosCommands request)
        {
            var response = await Mediator.Send(request);
            return Ok(response);
        }
        [Route("Update")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateContactInfosCommands request)
        {
            var response = await Mediator.Send(request);
            return Ok(response);
        }
        [Route("{Id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string Id)
        {
            var response = await Mediator.Send(new DeleteContactInfosCommands() { UUID = Id });
            return Ok(response);
        }
    }
}
