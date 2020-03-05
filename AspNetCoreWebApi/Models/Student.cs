using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApi.Models
{
    public class Student
    {
        public long StudentId { get; set; }
        public Person Person { get; set; }
        public TuitionAgency TuitionAgency { get; set; }
        public ICollection<Course> CoursesRegistered { get; set; }
    }
}
