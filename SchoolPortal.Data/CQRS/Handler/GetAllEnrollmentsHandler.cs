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
    public class GetAllEnrollmentsHandler : IRequestHandler<GetAllEnrollmentsQuery, IEnumerable<EnrollmentDto>>
    {
        private readonly IEnrollmentService _service;

        public GetAllEnrollmentsHandler(IEnrollmentService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<EnrollmentDto>> Handle(GetAllEnrollmentsQuery request, CancellationToken cancellationToken)
            => await _service.GetAllAsync();
    }
}
