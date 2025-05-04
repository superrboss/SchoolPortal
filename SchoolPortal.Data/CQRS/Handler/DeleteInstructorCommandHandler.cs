using MediatR;
using SchoolPortal.Data.Interface;
using SchoolPortal.Core.CQRS.Instructors.Commands;
using System.Threading;
using System.Threading.Tasks;
using SchoolPortal.Data.UnitOfWork;

namespace SchoolPortal.Core.CQRS.Instructors.Handlers
{
    public class DeleteInstructorCommandHandler : IRequestHandler<DeleteInstructorCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteInstructorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteInstructorCommand request, CancellationToken cancellationToken)
        {
            var instructor = await _unitOfWork.Instructors.GetByIdAsync(request.Id);
            if (instructor == null || instructor.IsDeleted) return false;

            instructor.IsDeleted = true;
            instructor.UpdatedBy = "Admin";
            instructor.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Instructors.Update(instructor); // soft delete
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
