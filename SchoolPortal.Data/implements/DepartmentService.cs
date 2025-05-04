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
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var departments = await _unitOfWork.Departments.GetAllAsync();
            // Optional: filter out deleted departments if not handled in repository
            departments = departments.Where(d => !d.IsDeleted).ToList();
            return _mapper.Map<IEnumerable<DepartmentDto>>(departments);
        }

        public async Task<DepartmentDto?> GetDepartmentByIdAsync(int id)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(id);
            return (department == null || department.IsDeleted)
                ? null
                : _mapper.Map<DepartmentDto>(department);
        }

        public async Task<DepartmentDto> CreateDepartmentAsync(DepartmentDto departmentDto)
        {
            // Validate AdministratorId
            if (departmentDto.AdministratorId != null)
            {
                var instructor = await _unitOfWork.Instructors.GetByIdAsync(departmentDto.AdministratorId.Value);

                if (instructor == null || instructor.IsDeleted)
                {
                    throw new Exception("Invalid AdministratorId. Instructor not found or is deleted.");
                }
            }

            var department = _mapper.Map<Department>(departmentDto);
            department.CreatedBy = "Admin";
            department.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.Departments.AddAsync(department); 
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<DepartmentDto>(department);
        }



        public async Task<bool> UpdateDepartmentAsync(DepartmentDto departmentDto)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(departmentDto.Id);
            if (department == null || department.IsDeleted) return false;

            _mapper.Map(departmentDto, department);
            department.UpdatedBy = "Admin";
            department.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Departments.Update(department);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(id);
            if (department == null || department.IsDeleted) return false;

            department.IsDeleted = true;
            department.UpdatedBy = "Admin";
            department.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Departments.Update(department); // soft delete
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
