using MediatR;
using SchoolPortal.Core.DTO;
using SchoolPortal.Data.CQRS.Queries;
using SchoolPortal.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.Data.CQRS.Handler
{
    public class GetAllCoursesHandler : IRequestHandler<GetAllCoursesQuery, IEnumerable<CourseDto>>
    {
        private readonly ICourseService _courseService;
        public GetAllCoursesHandler(ICourseService courseService) => _courseService = courseService;
        public async Task<IEnumerable<CourseDto>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
            => await _courseService.GetAllCoursesAsync();
    }
}
