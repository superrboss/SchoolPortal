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
    public class UpdateDepartmentHandler : IRequestHandler<UpdateDepartmentCommand, bool>
    {
        private readonly IDepartmentService _service;
        public UpdateDepartmentHandler(IDepartmentService service) => _service = service;
        public async Task<bool> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
            => await _service.UpdateDepartmentAsync(request.DepartmentDto);
    }
}
