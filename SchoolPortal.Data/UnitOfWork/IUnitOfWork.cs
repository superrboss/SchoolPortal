using SchoolPortal.Core.IRepo;
using SchoolPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Student> Students { get; }
        IGenericRepository<Course> Courses { get; }
        IGenericRepository<Enrollment> Enrollments { get; }
        IGenericRepository<Department> Departments { get; }
        IGenericRepository<Instructor> Instructors { get; }
        Task<int> CompleteAsync();
    }
}
