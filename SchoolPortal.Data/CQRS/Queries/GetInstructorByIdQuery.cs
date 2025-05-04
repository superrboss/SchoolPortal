using MediatR;
using SchoolPortal.Core.DTO;

namespace SchoolPortal.Core.CQRS.Instructors.Queries
{
    public record GetInstructorByIdQuery(int Id) : IRequest<InstructorDto?>;
}
