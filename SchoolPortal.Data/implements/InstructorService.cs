using AutoMapper;
using SchoolPortal.Core.DTO;
using SchoolPortal.Data.Interface;
using SchoolPortal.Data.Models;
using SchoolPortal.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal.Data.implements
{
    public class InstructorService : IInstructorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InstructorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Get All Instructors (excluding deleted ones)
        public async Task<IEnumerable<InstructorDto>> GetAllInstructorsAsync()
        {
            var instructors = await _unitOfWork.Instructors.GetAllAsync();
            // Filter out the deleted instructors
            var activeInstructors = instructors.Where(i => !i.IsDeleted).ToList();
            return _mapper.Map<IEnumerable<InstructorDto>>(activeInstructors);
        }

        // Get Instructor by Id (exclude deleted ones)
        public async Task<InstructorDto?> GetInstructorByIdAsync(int id)
        {
            var instructor = await _unitOfWork.Instructors.GetByIdAsync(id);
            if (instructor == null || instructor.IsDeleted) return null;

            return _mapper.Map<InstructorDto>(instructor);
        }

        // Create a new Instructor
        public async Task<InstructorDto> CreateInstructorAsync(InstructorDto instructorDto)
        {
            var instructor = _mapper.Map<Instructor>(instructorDto);
            instructor.CreatedBy = "Admin";
            instructor.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.Instructors.AddAsync(instructor);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<InstructorDto>(instructor);
        }

        // Update Instructor (ensure it's not deleted)
        public async Task<bool> UpdateInstructorAsync(InstructorDto instructorDto)
        {
            var instructor = await _unitOfWork.Instructors.GetByIdAsync(instructorDto.Id);
            if (instructor == null || instructor.IsDeleted) return false;

            // Map updates to the existing instructor
            _mapper.Map(instructorDto, instructor);
            instructor.UpdatedBy = "Admin";
            instructor.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Instructors.Update(instructor);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        // Soft delete an Instructor (mark as deleted)
        public async Task<bool> DeleteInstructorAsync(int id)
        {
            var instructor = await _unitOfWork.Instructors.GetByIdAsync(id);
            if (instructor == null || instructor.IsDeleted) return false;

            // Mark instructor as deleted
            instructor.IsDeleted = true;
            instructor.UpdatedBy = "Admin";
            instructor.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Instructors.Update(instructor);  // Soft delete operation
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
