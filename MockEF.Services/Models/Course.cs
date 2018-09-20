using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MockEF.Data.Models
{
    public class Course : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}