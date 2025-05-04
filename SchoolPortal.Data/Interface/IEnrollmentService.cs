using SchoolPortal.Core.DTO;
using SchoolPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.Data.Interface
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<EnrollmentDto>> GetAllAsync();
        Task<EnrollmentDto?> GetByIdAsync(int id);
        Task<EnrollmentDto> CreateAsync(EnrollmentDto dto);
        Task<bool> UpdateAsync(EnrollmentDto dto);
        Task<bool> DeleteAsync(int id);
    }

}
