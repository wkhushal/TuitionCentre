using System.Collections.Generic;

namespace AspNetCoreWebApi.Models
{
    public class Course
    {
        public long CourseId { get; set; }
        public string Name { get; set; }
        public long TuitionAgencyId { get; set; }
        public virtual TuitionAgency TuitionAgency { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
