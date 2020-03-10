using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApi.DTOs.Query
{
    public class StudentQueryDto
    {
        public long StudentId { get; set; }
        public PersonQueryDto Person { get; set; }
        public long TuitionAgencyId { get; set; }
        public ICollection<long> CoursesRegistered { get; set; }
    }
}
