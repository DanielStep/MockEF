using MockEF.Data.Enums;
using System;

namespace MockEF.Data.Models
{
    public class BaseModel
    {
        public Guid Id { get; set; }
        public RowStatus RowStatus { get; set; }
    }
}
