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
    public class DeleteCourseHandler : IRequestHandler<DeleteCourseCommand, bool>
    {
        private readonly ICourseService _courseService;
        public DeleteCourseHandler(ICourseService courseService) => _courseService = courseService;
        public async Task<bool> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
            => await _courseService.DeleteCourseAsync(request.Id);
    }
}
