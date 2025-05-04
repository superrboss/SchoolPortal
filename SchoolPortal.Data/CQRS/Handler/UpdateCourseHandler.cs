using MediatR;
using SchoolPortal.Data.CQRS.Commands;
using SchoolPortal.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.Data.CQRS.Handler
{
    public class UpdateCourseHandler : IRequestHandler<UpdateCourseCommand, bool>
    {
        private readonly ICourseService _courseService;
        public UpdateCourseHandler(ICourseService courseService) => _courseService = courseService;
        public async Task<bool> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
            => await _courseService.UpdateCourseAsync(request.CourseDto);
    }
}
