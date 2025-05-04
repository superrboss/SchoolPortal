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
    public class DeleteDepartmentHandler : IRequestHandler<DeleteDepartmentCommand, bool>
    {
        private readonly IDepartmentService _service;
        public DeleteDepartmentHandler(IDepartmentService service) => _service = service;
        public async Task<bool> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
            => await _service.DeleteDepartmentAsync(request.Id);
    }
}
