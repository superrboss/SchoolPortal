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
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CourseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _unitOfWork.Courses.GetAllAsync();
            courses = courses.Where(c => !c.IsDeleted).ToList(); // Exclude soft-deleted
            return _mapper.Map<IEnumerable<CourseDto>>(courses);
        }

        public async Task<CourseDto?> GetCourseByIdAsync(int id)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(id);
            return (course == null || course.IsDeleted)
                ? null
                : _mapper.Map<CourseDto>(course);
        }

        public async Task<CourseDto> CreateCourseAsync(CourseDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            course.CreatedBy = "Admin";
            course.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.Courses.AddAsync(course);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<CourseDto>(course);
        }

        public async Task<bool> UpdateCourseAsync(CourseDto courseDto)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(courseDto.Id);
            if (course == null || course.IsDeleted) return false;

            _mapper.Map(courseDto, course);
            course.UpdatedBy = "Admin";
            course.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Courses.Update(course);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(id);
            if (course == null || course.IsDeleted) return false;

            course.IsDeleted = true;
            course.UpdatedBy = "Admin";
            course.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Courses.Update(course); // Soft delete
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
