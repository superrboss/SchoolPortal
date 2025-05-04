using MediatR;
using SchoolPortal.Core.DTO;
using SchoolPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.Data.CQRS.Commands
{
    public record CreateStudentCommand(StudentDto StudentDto) : IRequest<StudentDto>;
}