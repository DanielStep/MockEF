using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockEF.Data.Enums
{
    public enum RowStatus : byte
    {
        Draft = 0,
        Active = 1,
        Inactive = 2,
        Deleted = 3,
        Modified = 4,
        Unchanged = 5
    }
}
