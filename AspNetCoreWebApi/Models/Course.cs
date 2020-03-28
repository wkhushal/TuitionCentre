using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApi.Models
{
    public class Course
    {
        public long CourseId { get; set; }
        public string Name { get; set; }
        public long TuitionAgencyId { get; set; }
        public ICollection<Subject> Subjects { get; set; }
    }
}
