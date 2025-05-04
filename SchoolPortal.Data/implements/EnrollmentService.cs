using AutoMapper;
using SchoolPortal.Core.DTO;
using SchoolPortal.Data.Interface;
using SchoolPortal.Data.Models;
using SchoolPortal.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.Data.implements
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EnrollmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EnrollmentDto>> GetAllAsync()
        {
            var enrollments = await _unitOfWork.Enrollments.GetAllAsync();
            enrollments = enrollments.Where(e => !e.IsDeleted).ToList();
            return _mapper.Map<IEnumerable<EnrollmentDto>>(enrollments);
        }

        public async Task<EnrollmentDto?> GetByIdAsync(int id)
        {
            var enrollment = await _unitOfWork.Enrollments.GetByIdAsync(id);
            return enrollment == null || enrollment.IsDeleted ? null : _mapper.Map<EnrollmentDto>(enrollment);
        }

        public async Task<EnrollmentDto> CreateAsync(EnrollmentDto dto)
        {
            var enrollment = _mapper.Map<Enrollment>(dto);
            enrollment.EnrollmentDate = DateTime.UtcNow;
            enrollment.CreatedBy = "Admin";
            enrollment.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.Enrollments.AddAsync(enrollment);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<EnrollmentDto>(enrollment);
        }

        public async Task<bool> UpdateAsync(EnrollmentDto dto)
        {
            var enrollment = await _unitOfWork.Enrollments.GetByIdAsync(dto.Id);
            if (enrollment == null || enrollment.IsDeleted) return false;

            _mapper.Map(dto, enrollment);
            enrollment.UpdatedBy = "Admin";
            enrollment.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Enrollments.Update(enrollment);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var enrollment = await _unitOfWork.Enrollments.GetByIdAsync(id);
            if (enrollment == null || enrollment.IsDeleted) return false;

            enrollment.IsDeleted = true;
            enrollment.UpdatedBy = "Admin";
            enrollment.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Enrollments.Update(enrollment);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }

}
