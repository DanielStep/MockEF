using MockEF.Data;
using MockEF.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockEF.Service
{
    public class MockEFService : IService
    {
        IDbContext DbContext;
        public MockEFService(IDbContext db)
        {
            DbContext = db;
        }

        public IEnumerable<Student> List()
        {
            var result = DbContext.Set<Student>();

            return result;
        }

        public Student Get(string name)
        {
            return DbContext.Set<Student>().Where(s => s.FirstMidName == name).FirstOrDefault();
        }

        public Enrollment GetEnrollment(int id)
        {
            return DbContext.Enrollments.Include("Student").Where(e => e.EnrollmentID == id).FirstOrDefault();
        }

        public IEnumerable<string> GetStudents()
        {
            return DbContext.Students.OrderBy(x => x.EnrollmentDate).Select(x => x.FirstMidName);
        }

        public bool HasStudentWithName(string lastName)
        {
            return DbContext.Students.Any(x => x.LastName == lastName);
        }

        public bool ContainsStudent(Student student)
        {
            return DbContext.Students.Contains(student);
        }

        public int AddStudent(Student student)
        {
            var result = DbContext.Students.Add(student);
            return DbContext.SaveChanges();
        }

        public int RemoveStudent(int id)
        {
            var student = DbContext.Students.SingleOrDefault(s => s.ID == id);
            var result = DbContext.Students.Remove(student);
            return DbContext.SaveChanges();
        }

        public List<Student> GetStudentsByCourseName(string courseName)
        {
            var students = DbContext.Students.Where(x => x.Enrollments.Any(e => e.Course.Title == courseName)).ToList();
            return students;
        }

    }
}
