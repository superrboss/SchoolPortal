using MediatR;
using SchoolPortal.Core.DTO;
using SchoolPortal.Data.CQRS.Queries;
using SchoolPortal.Data.Interface;
using SchoolPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.Data.CQRS.Handler
{
    public class GetStudentByIdHandler : IRequestHandler<GetStudentByIdQuery, StudentDto?>
    {
        private readonly IStudentService _studentService;
        public GetStudentByIdHandler(IStudentService studentService) => _studentService = studentService;
        public async Task<StudentDto?> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
            => await _studentService.GetStudentByIdAsync(request.Id);
    }
}
