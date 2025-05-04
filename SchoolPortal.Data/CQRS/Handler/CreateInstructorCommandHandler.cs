using AutoMapper;
using MediatR;
using SchoolPortal.Data.Interface;
using SchoolPortal.Core.CQRS.Instructors.Commands;
using SchoolPortal.Core.DTO;
using System.Threading;
using System.Threading.Tasks;
using SchoolPortal.Data.Models;
using SchoolPortal.Data.UnitOfWork;

namespace SchoolPortal.Core.CQRS.Instructors.Handlers
{
    public class CreateInstructorCommandHandler : IRequestHandler<CreateInstructorCommand, InstructorDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateInstructorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<InstructorDto> Handle(CreateInstructorCommand request, CancellationToken cancellationToken)
        {
            var instructor = _mapper.Map<Instructor>(request.InstructorDto);
            instructor.CreatedBy = "Admin";
            instructor.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.Instructors.AddAsync(instructor);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<InstructorDto>(instructor);
        }
    }
}
