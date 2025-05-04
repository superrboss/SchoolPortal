using AutoMapper;
using MediatR;
using SchoolPortal.Data.Interface;
using SchoolPortal.Core.CQRS.Instructors.Commands;
using System.Threading;
using System.Threading.Tasks;
using SchoolPortal.Data.UnitOfWork;

namespace SchoolPortal.Core.CQRS.Instructors.Handlers
{
    public class UpdateInstructorCommandHandler : IRequestHandler<UpdateInstructorCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateInstructorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateInstructorCommand request, CancellationToken cancellationToken)
        {
            var instructor = await _unitOfWork.Instructors.GetByIdAsync(request.InstructorDto.Id);
            if (instructor == null || instructor.IsDeleted) return false;

            _mapper.Map(request.InstructorDto, instructor);
            instructor.UpdatedBy = "Admin";
            instructor.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Instructors.Update(instructor);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
