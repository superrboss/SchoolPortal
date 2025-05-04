using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolPortal.Core.DTO;
using SchoolPortal.Data.CQRS.Commands;
using SchoolPortal.Data.CQRS.Queries;
using SchoolPortal.Data.Models;

namespace SchoolPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EnrollmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _mediator.Send(new GetAllEnrollmentsQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetEnrollmentByIdQuery(id));
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EnrollmentDto dto)
            => Ok(await _mediator.Send(new CreateEnrollmentCommand(dto)));

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EnrollmentDto dto)
        {
            var success = await _mediator.Send(new UpdateEnrollmentCommand(dto));
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _mediator.Send(new DeleteEnrollmentCommand(id));
            return success ? NoContent() : NotFound();
        }
    }

}
