using MockEF.Data;
using MockEF.Data.Models;
using MockEF.Data.Repository;
using MockEF.Service;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MockEF.Tests.Services
{
    [TestFixture]
    public class UnitOfWorkTests : ClassUnderTestGeneric<UnitOfWorkService>
    {
        [Test]
        public void Get_GivenValidGuid_ShouldReturnStudent()
        {
            var testGuid = Guid.NewGuid();
            var data = new List<Student>
            {
            new Student{Id=Guid.NewGuid(), FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
            new Student{Id=testGuid, FirstMidName= "Tony" ,LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
            };

            //Arrange
            var mockRepo = MockRepoOf<Student>();
            mockRepo.Setup(repo => repo.Find(testGuid)).Returns(data[1]);

            //Act
            var result = ClassUnderTest.Find(testGuid);
            //Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(testGuid);
        }
    }
}