using MediatR;
using SchoolPortal.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.Data.CQRS.Queries
{
    public record GetAllCoursesQuery() : IRequest<IEnumerable<CourseDto>>;
}
