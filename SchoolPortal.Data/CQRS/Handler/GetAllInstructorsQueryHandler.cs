using AutoMapper;
using MediatR;
using SchoolPortal.Data.Interface;
using SchoolPortal.Core.DTO;
using SchoolPortal.Core.CQRS.Instructors.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SchoolPortal.Data.UnitOfWork;

namespace SchoolPortal.Core.CQRS.Instructors.Handlers
{
    public class GetAllInstructorsQueryHandler : IRequestHandler<GetAllInstructorsQuery, IEnumerable<InstructorDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllInstructorsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InstructorDto>> Handle(GetAllInstructorsQuery request, CancellationToken cancellationToken)
        {
            var instructors = await _unitOfWork.Instructors.GetAllAsync();
            instructors = instructors.Where(i => !i.IsDeleted).ToList(); // Optional: filter out deleted instructors
            return _mapper.Map<IEnumerable<InstructorDto>>(instructors);
        }
    }
}
