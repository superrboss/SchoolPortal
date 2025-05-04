using AutoMapper;
using SchoolPortal.Core.DTO;
using SchoolPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.Data.Mapping
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<StudentDto, Student>().ReverseMap();
            CreateMap<AddressDto, Address>().ReverseMap();
            CreateMap<DepartmentDto, Department>().ReverseMap();
            CreateMap<InstructorDto, Instructor>().ReverseMap();
            CreateMap<CourseDto, Course>().ReverseMap();
            CreateMap<OfficeAssignmentDto, OfficeAssignment>().ReverseMap();
            CreateMap<Enrollment, EnrollmentDto>().ReverseMap();

        }
    }
}
