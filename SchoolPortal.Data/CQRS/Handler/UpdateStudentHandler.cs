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
    public class UpdateStudentHandler : IRequestHandler<UpdateStudentCommand, bool>
    {
        private readonly IStudentService _studentService;
        public UpdateStudentHandler(IStudentService studentService) => _studentService = studentService;
        public async Task<bool> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
            => await _studentService.UpdateStudentAsync(request.StudentDto);
    }
}
