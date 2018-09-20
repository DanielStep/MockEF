using MockEF.Data.Models;
using MockEF.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockEF.Data
{
    public interface IUnitOfWork
    {
        IReadWriteRepository<Student> Students { get; }
        IReadWriteRepository<Enrollment> Enrollments { get; }
        IReadWriteRepository<Course> Courses { get; }

        int SaveChanges();
    }
}
