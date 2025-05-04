using SchoolPortal.Core.IRepo;
using SchoolPortal.Data.DataBase;
using SchoolPortal.Data.Models;
using SchoolPortal.Data.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IGenericRepository<Student>? _students;
        private IGenericRepository<Course>? _courses;
        private IGenericRepository<Enrollment>? _enrollments;
        private IGenericRepository<Department>? _departments;
        private IGenericRepository<Instructor>? _instructors;

        public UnitOfWork(AppDbContext context) => _context = context;

        public IGenericRepository<Student> Students => _students ??= new GenericRepository<Student>(_context);
        public IGenericRepository<Course> Courses => _courses ??= new GenericRepository<Course>(_context);
        public IGenericRepository<Enrollment> Enrollments => _enrollments ??= new GenericRepository<Enrollment>(_context);
        public IGenericRepository<Department> Departments => _departments ??= new GenericRepository<Department>(_context);
        public IGenericRepository<Instructor> Instructors => _instructors ??= new GenericRepository<Instructor>(_context);

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
