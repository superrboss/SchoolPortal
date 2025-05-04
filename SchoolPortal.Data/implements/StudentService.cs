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
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            var students = await _unitOfWork.Students.GetAllAsync();
            students = students.Where(s => !s.IsDeleted).ToList(); // Ignore deleted
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }

        public async Task<StudentDto?> GetStudentByIdAsync(int id)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(id);
            return (student == null || student.IsDeleted)
                ? null
                : _mapper.Map<StudentDto>(student);
        }

        public async Task<StudentDto> CreateStudentAsync(StudentDto studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);
            student.CreatedBy = "Admin";
            student.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.Students.AddAsync(student);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<StudentDto>(student);
        }

        public async Task<bool> UpdateStudentAsync(StudentDto studentDto)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(studentDto.Id);
            if (student == null || student.IsDeleted) return false;

            _mapper.Map(studentDto, student);
            student.UpdatedBy = "Admin";
            student.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Students.Update(student);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(id);
            if (student == null || student.IsDeleted) return false;

            student.IsDeleted = true;
            student.UpdatedBy = "Admin";
            student.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Students.Update(student); // Soft delete
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
