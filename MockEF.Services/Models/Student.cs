﻿using System;
using System.Collections.Generic;

namespace MockEF.Data.Models
{
    public class Student : BaseModel
    {
        public int StudentID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}