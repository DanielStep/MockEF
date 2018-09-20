using MockEF.Data;
using MockEF.Data.Models;
using MockEF.Data.Repository;
using MockEF.Service;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MockEF.Tests.Services
{
    [TestFixture]
    public class UnitTestForClass
    {
        public UnitOfWorkService ClassUnderTest { get; set; }
        protected Mock<IDbContext> _dbContext;
        private Dictionary<string, object> _repos;

        [OneTimeSetUp]
        public void OneTime()
        {
            _dbContext = new Mock<IDbContext>();

            _repos = new Dictionary<string, object>();

            Mock<IReadWriteRepository<Student>> mockRepo = new Mock<IReadWriteRepository<Student>>();
            _repos.Add(typeof(Student).FullName, mockRepo);


            var uow = new UnitOfWork(_dbContext.Object, mockRepo.Object, null, null);
            ClassUnderTest = new UnitOfWorkService(uow);
        }

        protected void hassaved()
        {
            _dbContext.Verify(a => a.SaveChanges());
        }

        protected Mock<IReadWriteRepository<M>> MockRepoOf<M>() where M : BaseModel
        {
            try
            {
                return (Mock<IReadWriteRepository<M>>)_repos[typeof(M).FullName];
            }
            catch (KeyNotFoundException)
            {
                throw new Exception("No repo");
            }
        }
    }
}