using MediatR;
using SchoolPortal.Core.DTO;
using System.Collections.Generic;

namespace SchoolPortal.Core.CQRS.Instructors.Queries
{
    public record GetAllInstructorsQuery() : IRequest<IEnumerable<InstructorDto>>;
}
