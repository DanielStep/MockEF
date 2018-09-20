using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MockEF.Data.Models;

namespace MockEF.Data.Repository
{
    public interface IReadWriteRepository<T> where T : BaseModel
    {
        IDbSet<T> Set { get; }

        T Find(Guid id);
    }
}
