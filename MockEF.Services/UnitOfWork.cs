using MockEF.Data.Models;
using MockEF.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockEF.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContext _dbContext;
        
        public IReadWriteRepository<Student> Students { get; }
        public IReadWriteRepository<Enrollment> Enrollments { get; }
        public IReadWriteRepository<Course> Courses { get; }

        public UnitOfWork(IDbContext dbContext,
                          IReadWriteRepository<Student> students,
                          IReadWriteRepository<Enrollment> enrollments,
                          IReadWriteRepository<Course> courses)
        {
            Students = students;
            Enrollments = enrollments;
            Courses = courses;

            _dbContext = dbContext;
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
