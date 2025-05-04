using MediatR;
using SchoolPortal.Core.DTO;
using SchoolPortal.Data.CQRS.Commands;
using SchoolPortal.Data.Interface;
using SchoolPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.Data.CQRS.Handler
{
    public class CreateStudentHandler : IRequestHandler<CreateStudentCommand, StudentDto>
    {
        private readonly IStudentService _studentService;
        public CreateStudentHandler(IStudentService studentService) => _studentService = studentService;
        public async Task<StudentDto> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
            => await _studentService.CreateStudentAsync(request.StudentDto);
    }

}
