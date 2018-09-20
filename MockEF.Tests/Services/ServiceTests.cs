using System;
using NUnit.Framework;
using Moq;
using MockEF.Data;
using MockEF.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using MockEF.Service;
using Shouldly;
using MockEF.Tests.Data;

namespace MockEF.Tests.Services
{
    [TestFixture]
    public class ServiceTests
    {
        [Test]
        public void List_ReturnStudents()
        {
            var data = new List<Student>
            {
            new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
            new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
            };

            //Arrange
            var mockSet = GetQueryableMockDbSet(data.ToArray());

            var mockContext = new Mock<IDbContext>();

            mockContext.Setup(con => con.Set<Student>()).Returns(mockSet.Object);

            var classUnderTest = new MockEFService(mockContext.Object);

            //Act
            var result = classUnderTest.List();
            //Assert
            result.Count().ShouldBe(data.Count());
        }

        [Test]
        public void Get_GivenValidName_ShouldReturnStudent()
        {
            var testName = "Meredith";
            var data = new List<Student>
            {
            new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
            new Student{FirstMidName= testName ,LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
            };

            //Arrange
            var mockSet = GetQueryableMockDbSet(data.ToArray());

            var mockContext = new Mock<IDbContext>();

            mockContext.Setup(con => con.Set<Student>()).Returns(mockSet.Object);

            var classUnderTest = new MockEFService(mockContext.Object);

            //Act
            var result = classUnderTest.Get(testName);
            //Assert
            result.ShouldNotBeNull();
            result.FirstMidName.ShouldBe(testName);
        }

        [Test]
        public void Get_GivenInValidName_ShouldReturnNull()
        {
            var testName = "Meredith";
            var data = new List<Student>
            {
            new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
            new Student{FirstMidName= testName ,LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
            };

            //Arrange
            var mockSet = GetQueryableMockDbSet(data.ToArray());

            var mockContext = new Mock<IDbContext>();

            mockContext.Setup(con => con.Set<Student>()).Returns(mockSet.Object);

            var classUnderTest = new MockEFService(mockContext.Object);

            //Act
            var result = classUnderTest.Get("Invalid");
            //Assert
            result.ShouldBeNull();
        }

        [Test]
        public void GetEnrollment_GivenValidId_ShouldReturnEnrollmentIncludingStudent()
        {
            var testName = "Meredith";
            var data = new List<Enrollment>
            {
            new Enrollment{EnrollmentID=1, Student = new Student() { StudentID=1, FirstMidName=testName}, StudentID =1,CourseID=1050,Grade=Grade.A},
            new Enrollment{EnrollmentID=2, Student = new Student() { StudentID=2, FirstMidName="Daniel"}, StudentID=2,CourseID=4022,Grade=Grade.C},
            };

            //Arrange
            var mockSet = GetQueryableMockDbSet(data.ToArray());
            mockSet.Setup(x => x.Include(It.IsAny<string>())).Returns(mockSet.Object);
            mockSet.Setup(x => x.AsNoTracking()).Returns(mockSet.Object);

            var mockContext = new Mock<IDbContext>();

            mockContext.Setup(con => con.Enrollments).Returns(mockSet.Object);

            var classUnderTest = new MockEFService(mockContext.Object);

            //Act
            var result = classUnderTest.GetEnrollment(1);
            //Assert
            result.ShouldNotBeNull();
            result.Student.ShouldNotBeNull();
        }

        [Test]
        public void GetStudents_ShouldReturnNamesOrderedByDate()
        {
            var data = new List<Student>
            {
            new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
            new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
            new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
            };

            //Arrange
            var mockSet = GetQueryableMockDbSet(data.ToArray());
            mockSet.Setup(x => x.Include(It.IsAny<string>())).Returns(mockSet.Object);

            var mockContext = new Mock<IDbContext>();

            mockContext.Setup(con => con.Students).Returns(mockSet.Object);

            var classUnderTest = new MockEFService(mockContext.Object);

            //Act
            var result = classUnderTest.GetStudents();
            //Assert
            result.First().ShouldBe("Peggy");
        }

        [Test]
        public void GetEnrollment_ShouldReturnEnrollmentIncludingStudentOrderedByDate()
        {
            var data = new List<Student>
            {
            new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
            new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
            new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
            };

            //Arrange
            var mockSet = GetQueryableMockDbSet(data.ToArray());
            mockSet.Setup(x => x.Include(It.IsAny<string>())).Returns(mockSet.Object);

            var mockContext = new Mock<IDbContext>();

            mockContext.Setup(con => con.Students).Returns(mockSet.Object);

            var classUnderTest = new MockEFService(mockContext.Object);

            //Act
            var result = classUnderTest.GetStudents();
            //Assert
            result.First().ShouldBe("Peggy");
        }


        [Test]
        public void HasStudentWithName_ShouldReturnTrue()
        {
            var testName = "Justice";
            var data = new List<Student>
            {
            new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
            new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
            new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
            };

            //Arrange
            var mockSet = GetQueryableMockDbSet(data.ToArray());
            mockSet.Setup(x => x.Include(It.IsAny<string>())).Returns(mockSet.Object);

            var mockContext = new Mock<IDbContext>();

            mockContext.Setup(con => con.Students).Returns(mockSet.Object);

            var classUnderTest = new MockEFService(mockContext.Object);

            //Act
            var result = classUnderTest.HasStudentWithName(testName);
            //Assert
            result.ShouldBeTrue();
        }

        [Test]
        public void HasStudentWithName_ShouldReturnFalse()
        {
            var testName = "Salami";
            var data = new List<Student>
            {
            new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
            new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
            new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
            };

            //Arrange
            var mockSet = GetQueryableMockDbSet(data.ToArray());
            mockSet.Setup(x => x.Include(It.IsAny<string>())).Returns(mockSet.Object);

            var mockContext = new Mock<IDbContext>();

            mockContext.Setup(con => con.Students).Returns(mockSet.Object);

            var classUnderTest = new MockEFService(mockContext.Object);

            //Act
            var result = classUnderTest.HasStudentWithName(testName);
            //Assert
            result.ShouldBeFalse();
        }

        [Test]
        public void ContainsStudent_ShouldBeTrue()
        {
            var testStudent = new Student { FirstMidName = "Carson", LastName = "Alexander", EnrollmentDate = DateTime.Parse("2005-09-01") };
            var data = new List<Student>
            {
                testStudent,
                new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
                new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
            };

            //Arrange
            var mockSet = GetQueryableMockDbSet(data.ToArray());
            mockSet.Setup(x => x.Include(It.IsAny<string>())).Returns(mockSet.Object);

            var mockContext = new Mock<IDbContext>();

            mockContext.Setup(con => con.Students).Returns(mockSet.Object);

            var classUnderTest = new MockEFService(mockContext.Object);

            //Act
            var result = classUnderTest.ContainsStudent(testStudent);
            //Assert
            result.ShouldBeTrue();
        }

        [Test]
        public void ContainsStudent_ShouldBeFalse()
        {
            //Arrange
            var testStudent = new Student { FirstMidName = "Carson", LastName = "Alexander", EnrollmentDate = DateTime.Parse("2005-09-01") };
            var data = new List<Student>
            {
                new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
                new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
            };

            var mockSet = GetQueryableMockDbSet(data.ToArray());
            mockSet.Setup(x => x.Include(It.IsAny<string>())).Returns(mockSet.Object);

            var mockContext = new Mock<IDbContext>();

            mockContext.Setup(con => con.Students).Returns(mockSet.Object);

            var classUnderTest = new MockEFService(mockContext.Object);

            //Act
            var result = classUnderTest.ContainsStudent(testStudent);
            //Assert
            result.ShouldBeFalse();
        }

        [Test]
        public void AddStudent_GivenValidStudent_ShouldSaveAndAddOneStudent()
        {
            //Arrange
            var testStudent = new Student { FirstMidName = "Carson", LastName = "Alexander", EnrollmentDate = DateTime.Parse("2005-09-01") };

            var data = new List<Student>
            {
                new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
                new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
            };

            var mockSet = GetQueryableMockDbSet(data.ToArray());
            mockSet.Setup(x => x.Include(It.IsAny<string>())).Returns(mockSet.Object);
            mockSet.Setup(x => x.Add(testStudent)).Callback(() => data.Add(testStudent));
            var mockContext = new Mock<IDbContext>();
            mockContext.Setup(con => con.Students).Returns(mockSet.Object);
            mockContext.Setup(con => con.SaveChanges()).Returns(1);

            var classUnderTest = new MockEFService(mockContext.Object);

            //Action
            var result = classUnderTest.AddStudent(testStudent);

            //Assert
            result.ShouldBe(1);
            data.Count.ShouldBe(3);
            data.Any(x => x.LastName == testStudent.LastName).ShouldBe(true);
            mockContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void RemoveStudent_GivenValidStudentId_ShouldSaveAndRemoveOneStudent()
        {
            //Arrange
            var data = new List<Student>
            {
                new Student{StudentID = 1, FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
                new Student{StudentID = 2, FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
            };
            var testId = 1;
            var testStudent = data.SingleOrDefault(s => s.StudentID == testId);

            var mockSet = GetQueryableMockDbSet(data.ToArray());
            mockSet.Setup(x => x.Remove(testStudent)).Callback(() => data.Remove(testStudent));
            var mockContext = new Mock<IDbContext>();
            mockContext.Setup(con => con.Students).Returns(mockSet.Object);
            mockContext.Setup(con => con.SaveChanges()).Returns(1);

            var classUnderTest = new MockEFService(mockContext.Object);

            //Act
            var result = classUnderTest.RemoveStudent(data.First().StudentID);

            //Assert
            result.ShouldBe(1);
            data.Count.ShouldBe(1);
            data.Any(s => s.StudentID == testStudent.StudentID).ShouldBe(false);
            mockContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void RemoveStudent_GivenInvalidId_ShouldNotSaveAndRemoveNone()
        {
            //Arrange
            var data = new List<Student>
            {
                new Student{StudentID = 1, FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
                new Student{StudentID = 2, FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
            };
            var testId = 0;
            var testStudent = data.SingleOrDefault(s => s.StudentID == testId);

            var mockSet = GetQueryableMockDbSet(data.ToArray());
            mockSet.Setup(x => x.Remove(testStudent)).Callback(() => data.Remove(testStudent));
            var mockContext = new Mock<IDbContext>();
            mockContext.Setup(con => con.Students).Returns(mockSet.Object);
            mockContext.Setup(con => con.SaveChanges()).Returns(0);

            var classUnderTest = new MockEFService(mockContext.Object);

            //Act
            var result = classUnderTest.RemoveStudent(data.First().StudentID);

            //Assert
            result.ShouldBe(0);
            data.Count.ShouldBe(2);
            mockContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void GetStudentsByCourseName_GivenValidCourseName_ShouldReturnStudents()
        {
            //Arrange
            var data = FakeData.FakeStudentData;

            var mockSet = GetQueryableMockDbSet(data.ToArray());
            var mockContext = new Mock<IDbContext>();
            mockContext.Setup(con => con.Students).Returns(mockSet.Object);
            var classUnderTest = new MockEFService(mockContext.Object);

            //Act

            var resultMicro = classUnderTest.GetStudentsByCourseName("Microeconomics");
            var resultChem = classUnderTest.GetStudentsByCourseName("Chemistry");
            var resultComp = classUnderTest.GetStudentsByCourseName("Composition");

            //Assert

            resultMicro.Count().ShouldBe(2);
            resultMicro.All(s => s.Enrollments.Any(e => e.Course.Title == "Microeconomics")).ShouldBeTrue();
            resultChem.Count().ShouldBe(3);
            resultChem.All(s => s.Enrollments.Any(e => e.Course.Title == "Chemistry")).ShouldBeTrue();
            resultComp.Count().ShouldBe(1);
            resultComp.All(s => s.Enrollments.Any(e => e.Course.Title == "Composition")).ShouldBeTrue();
        }

        [Test]
        public void GetStudentsByCourseName_GivenValidCourseName_ShouldReturnNoStudents()
        {
            //Arrange
            var data = FakeData.FakeStudentData;

            var mockSet = GetQueryableMockDbSet(data.ToArray());
            var mockContext = new Mock<IDbContext>();
            mockContext.Setup(con => con.Students).Returns(mockSet.Object);
            var classUnderTest = new MockEFService(mockContext.Object);

            //Act

            var result = classUnderTest.GetStudentsByCourseName("Invalid");

            //Assert

            result.ShouldBeEmpty();
        }


        private static Mock<DbSet<T>> GetQueryableMockDbSet<T>(params T[] sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            return dbSet;
        }
    }
}
