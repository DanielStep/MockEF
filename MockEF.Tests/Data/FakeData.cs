using MockEF.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockEF.Tests.Data
{
    public class FakeData
    {
        internal static List<Student> FakeStudentData = new List<Student>
            {
                new Student
                {
                    StudentID = 1,
                    FirstMidName ="Peggy",
                    LastName ="Justice",
                    EnrollmentDate =DateTime.Parse("2001-09-01"),
                    Enrollments = new List<Enrollment>
                    {
                        new Enrollment
                        {
                            StudentID =1,
                            CourseID =1050,
                            Grade =Grade.A,
                            Course = new Course{CourseID=1050,Title="Chemistry",Credits=3}
                        },
                        new Enrollment
                        {
                            StudentID =1,
                            CourseID =4022,
                            Grade =Grade.C,
                            Course = new Course{CourseID=4022,Title="Microeconomics",Credits=3,}
                        },
                    }
                },
                new Student
                {
                    StudentID = 2,
                    FirstMidName ="Meredith",
                    LastName ="Alonso",
                    EnrollmentDate=DateTime.Parse("2002-09-01"),
                    Enrollments = new List<Enrollment>
                    {
                        new Enrollment
                        {
                            StudentID =2,
                            CourseID =1050,
                            Grade =Grade.A,
                            Course = new Course{CourseID=1050,Title="Chemistry",Credits=3}
                        },
                        new Enrollment
                        {
                            StudentID =2,
                            CourseID =4022,
                            Grade =Grade.C,
                            Course = new Course{CourseID=4022,Title="Microeconomics",Credits=3,}
                        },
                    }
                },
                new Student
                {
                    StudentID = 3,
                    FirstMidName ="Patrick",
                    LastName ="Emerton",
                    EnrollmentDate=DateTime.Parse("2002-09-01"),
                    Enrollments = new List<Enrollment>
                    {
                        new Enrollment
                        {
                            StudentID =3,
                            CourseID =1050,
                            Grade =Grade.A,
                            Course = new Course{CourseID=1050,Title="Chemistry",Credits=3}
                        },
                        new Enrollment
                        {
                            StudentID =3,
                            CourseID =2021,
                            Grade =Grade.C,
                            Course = new Course{CourseID=2021,Title="Composition",Credits=3,}
                        },
                    }
                },
            };
    }
}
