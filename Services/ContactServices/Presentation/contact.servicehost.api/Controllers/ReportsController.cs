using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc; 
using contact.application.Handlers.Reports.Queries; 

namespace contact.servicehost.api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [EnableCors("AllowOrigin")]
    [Produces("application/json")]

    public class ReportsController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [Route("{Id}")]
        [HttpGet]
        public async Task<IActionResult> Get(string Id)
        {
            var response = await Mediator.Send(new GetByIdReportsQuery() { Id = Id });
            return Ok(response);
        }
       
    }
}
