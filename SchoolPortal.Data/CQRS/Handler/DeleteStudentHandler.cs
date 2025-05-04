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
    public class DeleteStudentHandler : IRequestHandler<DeleteStudentCommand, bool>
    {
        private readonly IStudentService _studentService;
        public DeleteStudentHandler(IStudentService studentService) => _studentService = studentService;
        public async Task<bool> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
            => await _studentService.DeleteStudentAsync(request.Id);
    }
}
