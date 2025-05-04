using MediatR;
using SchoolPortal.Core.DTO;
using SchoolPortal.Data.CQRS.Queries;
using SchoolPortal.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.Data.CQRS.Handler
{
    public class GetDepartmentByIdHandler : IRequestHandler<GetDepartmentByIdQuery, DepartmentDto?>
    {
        private readonly IDepartmentService _service;
        public GetDepartmentByIdHandler(IDepartmentService service) => _service = service;
        public async Task<DepartmentDto?> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
            => await _service.GetDepartmentByIdAsync(request.Id);
    }
}
