﻿using MediatR;
using SchoolPortal.Core.DTO;

namespace SchoolPortal.Core.CQRS.Instructors.Commands
{
    public record CreateInstructorCommand(InstructorDto InstructorDto) : IRequest<InstructorDto>;
}
