using MediatR;
using SchoolPortal.Core.DTO;
using SchoolPortal.Data.CQRS.Commands;
using SchoolPortal.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.Data.CQRS.Handler
{
    public class CreateCourseHandler : IRequestHandler<CreateCourseCommand, CourseDto>
    {
        private readonly ICourseService _courseService;
        public CreateCourseHandler(ICourseService courseService) => _courseService = courseService;
        public async Task<CourseDto> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
            => await _courseService.CreateCourseAsync(request.CourseDto);
    }
}
