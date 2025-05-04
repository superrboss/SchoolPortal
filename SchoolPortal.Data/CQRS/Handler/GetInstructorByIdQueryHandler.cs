using AutoMapper;
using MediatR;
using SchoolPortal.Data.Interface;
using SchoolPortal.Core.DTO;
using SchoolPortal.Core.CQRS.Instructors.Queries;
using System.Threading;
using System.Threading.Tasks;
using SchoolPortal.Data.UnitOfWork;

namespace SchoolPortal.Core.CQRS.Instructors.Handlers
{
    public class GetInstructorByIdQueryHandler : IRequestHandler<GetInstructorByIdQuery, InstructorDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetInstructorByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<InstructorDto?> Handle(GetInstructorByIdQuery request, CancellationToken cancellationToken)
        {
            var instructor = await _unitOfWork.Instructors.GetByIdAsync(request.Id);
            return instructor == null || instructor.IsDeleted ? null : _mapper.Map<InstructorDto>(instructor);
        }
    }
}
