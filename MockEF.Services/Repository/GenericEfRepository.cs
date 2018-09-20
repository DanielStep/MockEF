using MockEF.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockEF.Data.Repository
{
    public class GenericEfRepository<T>: IReadWriteRepository<T> where T : BaseModel
    {
        private readonly IDbContext _context;
        public IDbSet<T> Set { get; }


        public GenericEfRepository(IDbContext context)
        {
            _context = context;
            Set = _context.Set<T>();
        }

        public T Find(Guid id)
        {
            return Set.FirstOrDefault(x => x.Id == id);
        }
    }
}
