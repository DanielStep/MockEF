using MockEF.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MockEF.Service
{
    public interface IService
    {
        IEnumerable<Student> List();
    }
}
