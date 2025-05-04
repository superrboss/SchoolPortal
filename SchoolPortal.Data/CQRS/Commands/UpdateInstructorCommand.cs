using MediatR;
using SchoolPortal.Core.DTO;

namespace SchoolPortal.Core.CQRS.Instructors.Commands
{
    public record UpdateInstructorCommand(InstructorDto InstructorDto) : IRequest<bool>;
}
