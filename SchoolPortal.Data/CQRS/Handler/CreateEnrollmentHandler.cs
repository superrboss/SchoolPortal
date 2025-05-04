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
    public class CreateEnrollmentHandler : IRequestHandler<CreateEnrollmentCommand, EnrollmentDto>
    {
        private readonly IEnrollmentService _service;

        public CreateEnrollmentHandler(IEnrollmentService service)
        {
            _service = service;
        }

        public async Task<EnrollmentDto> Handle(CreateEnrollmentCommand request, CancellationToken cancellationToken)
            => await _service.CreateAsync(request.EnrollmentDto);
    }
}
