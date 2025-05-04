using MediatR;

namespace SchoolPortal.Core.CQRS.Instructors.Commands
{
    public record DeleteInstructorCommand(int Id) : IRequest<bool>;
}
