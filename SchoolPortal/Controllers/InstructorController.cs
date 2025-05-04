using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolPortal.Core.CQRS.Instructors.Commands;
using SchoolPortal.Core.CQRS.Instructors.Queries;
using SchoolPortal.Core.DTO;
using System.Threading.Tasks;

namespace SchoolPortal.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstructorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InstructorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInstructors()
        {
            var result = await _mediator.Send(new GetAllInstructorsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInstructorById(int id)
        {
            var result = await _mediator.Send(new GetInstructorByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInstructor([FromBody] InstructorDto instructorDto)
        {
            var result = await _mediator.Send(new CreateInstructorCommand(instructorDto));
            return CreatedAtAction(nameof(GetInstructorById), new { id = result.Id }, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateInstructor([FromBody] InstructorDto instructorDto)
        {
            var result = await _mediator.Send(new UpdateInstructorCommand(instructorDto));
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstructor(int id)
        {
            var result = await _mediator.Send(new DeleteInstructorCommand(id));
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
