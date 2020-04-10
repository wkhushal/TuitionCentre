using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApi.Models
{
    public class Student
    {
        public long StudentId { get; set; }
        public virtual Person Person { get; set; }
        public virtual TuitionAgency TuitionAgency { get; set; }
        public virtual ICollection<Course> CoursesRegistered { get; set; }
    }
}
