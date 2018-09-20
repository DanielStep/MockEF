using MockEF.Data;
using MockEF.Data.Models;
using MockEF.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockEF.Service
{
    public class UnitOfWorkService
    {
        private IUnitOfWork _uow;

        public UnitOfWorkService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Student Find(Guid id)
        {
            var result = _uow.Students.Find(id);

            return result;
        }
    }
}
