using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MockEF.Data.Models;

namespace MockEF.Data
{
    public class Context : DbContext, IDbContext
    {
        public Context() : base("ContextConnectionString")
        {
            Database.SetInitializer<Context>(null);
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}
