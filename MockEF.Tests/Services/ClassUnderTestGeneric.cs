using MockEF.Data;
using MockEF.Data.Models;
using MockEF.Data.Repository;
using MockEF.Service;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MockEF.Tests.Services
{
    [TestFixture]
    public class ClassUnderTestGeneric<T>
    {
        public T ClassUnderTest { get; set; }
        protected Mock<IDbContext> _dbContext;
        private Dictionary<string, object> _params;
        private Dictionary<string, object> _UoWProperties;

        [OneTimeSetUp]
        public void OneTime()
        {
            _params = new Dictionary<string, object>();
            _UoWProperties = new Dictionary<string, object>();

            var ctors = typeof(T).GetConstructors();
            var ctor = ctors[0];

            foreach (var param in ctor.GetParameters())
            {
                //For value type and non interface params that are not DI
                if (!param.ParameterType.IsInterface)
                {
                    _params.Add(param.ParameterType.Name, param.ParameterType.IsValueType ? Activator.CreateInstance(param.ParameterType) : null);
                    continue;
                }

                try
                {
                    if (typeof(IUnitOfWork).IsAssignableFrom(param.ParameterType))
                    {
                        foreach (PropertyInfo property in param.ParameterType.GetProperties())
                        {
                            _UoWProperties.Add(property.Name, DynamicMock(property.PropertyType));
                        }

                        var UoWParams = GetUnitofWorkParameters();

                        UnitOfWork UoW = (UnitOfWork)typeof(UnitOfWork).GetConstructor(UoWParams.Select(x => x.GetType()).ToArray()).Invoke(UoWParams.ToArray());
                        _params.Add(param.ParameterType.Name, UoW);
                    }
                    else
                    {
                        _params.Add(param.ParameterType.Name, DynamicMock(param.ParameterType));
                    }
                }
                catch (ArgumentException e)
                {
                    throw new Exception($"This is probably because the ClassUnderTest has a dependency injected more than once :{param.ParameterType.Name}.", e);
                }
            }

            try
            {
                ClassUnderTest = (T)Activator.CreateInstance(typeof(T), _params.Values.Select(Convert).ToArray());
            }
            catch (MissingMethodException)
            {
                throw new Exception("This is probably because you have mocked a dependency that your ClassUnderTest doesn't have in the constructor.");
            }
        }

        private List<object> GetUnitofWorkParameters()
        {
            var UoWParams = new List<object>();
            var mockDb = new Mock<IDbContext>();
            UoWParams.Add(mockDb.Object);
            UoWParams.AddRange(_UoWProperties.Values.Select(Convert).ToArray());
            return UoWParams;
        }

        private static object DynamicMock(Type type)
        {
            return typeof(Mock<>).MakeGenericType(type).GetConstructor(Type.EmptyTypes)?.Invoke(new object[] { });
        }

        protected Mock<IReadWriteRepository<M>> MockRepoOf<M>() where M : BaseModel
        {
            try
            {
                return (Mock<IReadWriteRepository<M>>)_UoWProperties[typeof(M).Name + "s"];
            }
            catch (KeyNotFoundException)
            {
                throw new Exception("No repo");
            }
        }

        private static object Convert(object ob)
        {
            if (ob == null)
                return null;

            if (ob.GetType() == typeof(UnitOfWork))
                return ob;

            return ob.GetType().GetProperties().First(f => f.Name == "Object" && f.ReflectedType != typeof(object)).GetValue(ob, new object[] { });
        }
    }
}
