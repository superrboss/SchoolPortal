using SchoolPortal.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.Data.Interface
{
    public interface IInstructorService
    {
        Task<IEnumerable<InstructorDto>> GetAllInstructorsAsync();
        Task<InstructorDto?> GetInstructorByIdAsync(int id);
        Task<InstructorDto> CreateInstructorAsync(InstructorDto instructorDto);
        Task<bool> UpdateInstructorAsync(InstructorDto instructorDto);
        Task<bool> DeleteInstructorAsync(int id);
    }
}
