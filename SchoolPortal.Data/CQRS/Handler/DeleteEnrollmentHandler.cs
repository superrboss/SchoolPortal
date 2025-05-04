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
    public class DeleteEnrollmentHandler : IRequestHandler<DeleteEnrollmentCommand, bool>
    {
        private readonly IEnrollmentService _service;

        public DeleteEnrollmentHandler(IEnrollmentService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(DeleteEnrollmentCommand request, CancellationToken cancellationToken)
            => await _service.DeleteAsync(request.Id);
    }
}
