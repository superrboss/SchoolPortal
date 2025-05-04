using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.Data.CQRS.Commands
{
    public record DeleteCourseCommand(int Id) : IRequest<bool>;
}
