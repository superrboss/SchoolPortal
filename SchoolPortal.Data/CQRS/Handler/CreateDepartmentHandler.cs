using MediatR;
using SchoolPortal.Core.DTO;
using SchoolPortal.Data.CQRS.Commands;
using SchoolPortal.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.Data.CQRS.Handler
{
    public class CreateDepartmentHandler : IRequestHandler<CreateDepartmentCommand, DepartmentDto>
    {
        private readonly IDepartmentService _service;
        public CreateDepartmentHandler(IDepartmentService service) => _service = service;
        public async Task<DepartmentDto> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
            => await _service.CreateDepartmentAsync(request.DepartmentDto);
    }
}
